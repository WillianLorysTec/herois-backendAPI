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
        //public Heroi CriarHeroi(HeroiDTO heroi)
        //{

        //    using (var transaction = _dados.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            Heroi heroi_entidade = new()
        //            {
        //                Id = null,
        //                Nome = heroi.Nome,
        //                NomeHeroi = heroi.NomeHeroi,
        //                Peso = heroi.Peso,
        //                Altura = heroi.Altura

        //            };

        //            _dados.Herois.Add(heroi_entidade);
        //            _dados.SaveChanges();

        //            transaction.Commit();

        //            // não tenho tanto conhecimento em entity, acabei por buscar o registro inserido por nome

        //            Heroi? heroiInserido = _dados.Herois.FirstOrDefault(h => h.NomeHeroi == heroi.NomeHeroi);

        //            if (heroiInserido != null)
        //            {
        //                InserirPoderes(heroiInserido, heroi.PoderesAdicionados);

        //                return heroiInserido;

        //            }
        //            else
        //            {
        //                throw new Exception("Registro não encontrado após a inserção.");
        //            }


        //        }
        //        catch (Exception ex)
        //        {
        //            transaction.Rollback();
        //            throw new Exception("Erro ao criar heroi" + ex.Message);
        //        }
        //    }

        //}


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
                        Peso = heroi.Peso,
                        Altura = heroi.Altura
                    };
                    // perguntar se já existe o heroi
                    Heroi? existeHeroi = _dados.Herois.FirstOrDefault(i => i.NomeHeroi == heroi.NomeHeroi);
                    if (existeHeroi != null) 
                    {
                        return Result.Fail($"Não é possível inserir mais de um {heroi.NomeHeroi}, imagine a confusão que isso poderia gerar");
                    }

                    _dados.Herois.Add(heroi_entidade);
                    _dados.SaveChanges();

                    transaction.Commit();

                    // Busque o registro inserido pelo Id, que é o valor gerado automaticamente
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
                    return Result.Fail("Eita parece que estamos sendo muito utilizado: " + co.Message);
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
        public bool ExcluirHeroi(int idHeroi)
        {
            using (var transaction = _dados.Database.BeginTransaction())
            {
                try
                {
                    Heroi? heroi = _dados.Herois.FirstOrDefault(h => h.Id == idHeroi);

                    if (heroi != null)
                    {
                        _dados.Herois.Remove(heroi);
                        _dados.SaveChanges();

                        transaction.Commit();
                        return true;
                    }
                    else
                    {
                        transaction.Rollback();
                        return false;  // com o fluent result tratar **não encontramos nenhum heroi
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao excluir herói", ex);
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
                        SuperPoderes = heroi.SuperPoderes.Select(sp => new SuperpoderViewModel
                        {
                            Id = sp.Id,
                            Nome = sp.Superpoder
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

        public void AtualizarHeroi(HeroiDTO heroi)
        {
            try
            {
                using (var transaction = _dados.Database.BeginTransaction())
                {
                    Heroi? heroi_entidade = _dados.Herois.FirstOrDefault(i => i.Id == heroi.Id);

                    if (heroi_entidade != null)
                    {
                        heroi_entidade.Nome = heroi.Nome;
                        heroi_entidade.NomeHeroi = heroi.NomeHeroi;
                        heroi_entidade.Peso = heroi.Peso;
                        heroi_entidade.Altura = heroi.Altura;
                        heroi_entidade.DataNascimento = heroi.DataNascimento;

                        _dados.Update(heroi_entidade);

                        transaction.Commit();
                        AtualizarPoderes(heroi.PoderesAdicionados, heroi_entidade);

                        _dados.SaveChanges();


                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void AtualizarPoderes(List<int> poderesParaAdicionar, Heroi heroi_entidade)
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

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao adionar poderes", ex);
                }
            }

        }
    }
}
