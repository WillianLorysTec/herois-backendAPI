using FluentResults;
using Interfaces.Repositorio;
using Microsoft.EntityFrameworkCore;
using Models.DTOs;
using Models.Entidades;
using Models.ViewModel;

namespace Repositorio
{
    public class FluxoHeroiRepositorio : IFluxoHeroiRepositorio
    {
        private readonly AcessoDados _dados;

        public FluxoHeroiRepositorio(AcessoDados dados)
        {
            _dados = dados;
        }
        // POR BOA PRÁTICA, NÃO VOU USAR DO DISPOSE MANUAL NO ENTITY POIS JÁ INJETEI A DEPENDÊNCIA
        // E O CONTAINER DE INJEÇÃO DEVE CUIDAR DISTO. (acredito eu)

        public Result CriarHeroi(HeroiDTO heroi)
        {
            using (var transaction = _dados.Database.BeginTransaction())
            {
                try
                {
                    Heroi heroi_entidade = new()
                    {
                        Id = null,
                        Nome = heroi.Nome,
                        NomeHeroi = heroi.NomeHeroi,
                        DataNascimento = heroi.DataNascimento,
                        Peso = heroi.Peso,
                        Altura = heroi.Altura
                    };

                    Heroi? existeHeroi = _dados.Herois.FirstOrDefault(i => i.NomeHeroi == heroi.NomeHeroi);
                    if (existeHeroi != null)
                    {
                        return Result.Fail($"Não é possível inserir mais de um {heroi.NomeHeroi.ToUpper()}, imagine a confusão que isso poderia gerar");
                    }

                    _dados.Herois.Add(heroi_entidade);
                    _dados.SaveChanges();

                    transaction.Commit();

                    Heroi? heroiInserido = _dados.Herois.FirstOrDefault(h => h.Id == heroi_entidade.Id);

                    if (heroiInserido != null)
                    {
                        InserirPoderes(heroiInserido, heroi.PoderesAdicionados);

                        return Result.Ok();
                    }
                    else
                    {
                        return Result.Fail("Herói não encontrado!");
                    }
                }
                catch (DbUpdateConcurrencyException co)
                {
                    transaction.Rollback();
                    return Result.Fail("Eita parece que estamos sobrecarregados: " + co.Message);
                }
                catch (DbUpdateException up)
                {
                    transaction.Rollback();
                    return Result.Fail("Erro ao tentar salvar o heroi: " + up.Message);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return Result.Fail("Erro ao criar heroi: " + ex.Message);
                }
            }
        }




        private void InserirPoderes(Heroi heroiInserido, List<int> poderes)
        {
            using (var transaction = _dados.Database.BeginTransaction())
            {

                try
                {
                    var poderesFiltrados = _dados.Poder
                       .Where(p => poderes.Contains(p.Id))
                       .ToList();

                    heroiInserido.SuperPoderes.AddRange(poderesFiltrados);

                    _dados.SaveChanges();
                    transaction.Commit();

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao criar heroi" + ex.Message);
                }
            }
        }
        public Result ExcluirHeroi(int idHeroi)
        {
            using (var transaction = _dados.Database.BeginTransaction())
            {
                try
                {

                    Heroi? heroi = _dados.Herois.Include(h => h.SuperPoderes)
                        .FirstOrDefault(h => h.Id == idHeroi);

                    if (heroi != null)
                    {

                        heroi.SuperPoderes.Clear();

                        _dados.Herois.Remove(heroi);

                        _dados.SaveChanges();

                        transaction.Commit();

                        return Result.Ok();
                    }
                    else
                    {
                        transaction.Rollback();
                        return Result.Fail("Herói não encontrado!");
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return Result.Fail($"Erro ao excluir heroi: {ex.Message}");
                }
            }
        }


        public HeroiViewModel ListarHeroiPorID(int idHeroi)
        {
            try
            {
                HeroiViewModel? heroisAbaAtualizacao = _dados.Herois
                    .AsNoTracking()
                    .Where(id => id.Id == idHeroi)
                    .Select(heroi => new HeroiViewModel
                    {
                        Id = heroi.Id,
                        Nome = heroi.Nome,
                        NomeHeroi = heroi.NomeHeroi,
                        Peso = heroi.Peso,
                        Altura = heroi.Altura,
                        DataNascimento = heroi.DataNascimento,
                        SuperPoderes = heroi.SuperPoderes.Select(sp => new SuperpoderViewModel
                        {
                            Id = sp.Id,
                            Superpoder = sp.Superpoder
                        })
                    })
                    .FirstOrDefault();

                return heroisAbaAtualizacao ?? new HeroiViewModel();

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar heróis", ex);
            }

        }

        public List<Heroi> ListarHerois()
        {
            try
            {
                return _dados.Herois
                .AsNoTracking()
                .ToList();

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar heróis", ex);
            }
        }

        public List<Superpoderes> ListarPoderes()
        {
            try
            {
                List<Superpoderes>? poderes = _dados.Poder.AsNoTracking().ToList();

                return poderes;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar poderes", ex);
            }
        }

        public Result AtualizarHeroi(HeroiDTO heroi)
        {
            using (var transaction = _dados.Database.BeginTransaction())
            {
                try
                {

                    Heroi? heroi_entidade = _dados.Herois.FirstOrDefault(i => i.Id == heroi.Id);

                    if (heroi_entidade != null)
                    {
                        heroi_entidade.Nome = heroi.Nome;
                        heroi_entidade.Peso = heroi.Peso;
                        heroi_entidade.Altura = heroi.Altura;
                        heroi_entidade.DataNascimento = heroi.DataNascimento;

                        _dados.Update(heroi_entidade);

                        transaction.Commit();
                        Result poderesAtualizados = AtualizarPoderes(heroi.PoderesAdicionados, heroi_entidade);

                        if (poderesAtualizados.IsSuccess)
                        {
                            _dados.SaveChanges();
                            return Result.Ok();

                        }
                        else
                        {
                            transaction.Rollback();
                            return Result.Fail("Erro genérico.. revertendo.");

                        }

                    }
                    else
                    {
                        return Result.Fail("Herói não encontrado!");
                    }
                }
                catch (DbUpdateConcurrencyException co)
                {
                    transaction.Rollback();
                    return Result.Fail("Eita parece que estamos sobrecarregados: " + co.Message);
                }
                catch (DbUpdateException up)
                {
                    transaction.Rollback();
                    return Result.Fail("Erro ao tentar atualizar poderes do heroi: " + up.Message);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return Result.Fail("Erro ao atualizar poder do heroi: " + ex.Message);
                }
            }

        }

        private Result AtualizarPoderes(List<int> poderesParaAdicionar, Heroi heroi_entidade)
        {
            using (var transaction = _dados.Database.BeginTransaction())
            {
                try
                {
                    var heroi = _dados.Herois
                     .Include(h => h.SuperPoderes)
                     .Single(h => h.Id == heroi_entidade.Id);

                    List<Superpoderes> poderesAtualizados = _dados.Poder
                        .Where(p => poderesParaAdicionar.Contains(p.Id))
                        .ToList();

                    // Remove os superpoderes antigos que não estão na lista de novos poderes
                    var superPoderesParaRemover = heroi.SuperPoderes
                        .Where(sp => !poderesAtualizados.Any(np => np.Id == sp.Id))
                        .ToList();

                    foreach (var poderAntigo in superPoderesParaRemover)
                    {
                        heroi.SuperPoderes.Remove(poderAntigo);
                    }

                    // Adiciona os novos superpoderes
                    heroi.SuperPoderes.AddRange(poderesAtualizados);

                    _dados.SaveChanges();
                    transaction.Commit();

                    return Result.Ok();

                }
                catch (DbUpdateConcurrencyException co)
                {
                    transaction.Rollback();
                    return Result.Fail("Eita parece que estamos sobrecarregados: " + co.Message);
                }
                catch (DbUpdateException up)
                {
                    transaction.Rollback();
                    return Result.Fail("Erro ao tentar atualizar poderes do heroi: " + up.Message);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return Result.Fail("Erro ao atualizar poder do heroi: " + ex.Message);
                }
            }

        }
    }
}
