# ControlePontos
O programa foi feito em C# usando o banco de dados MySQL

1) Program.cs, altere a ConnectionString que está instanciada originalmente pelo o destino desejado. (Recomendado o uso de MySQL)

2) Dentro do Db de sua, crie a tabela que irá receber os resultados da seguinte forma:
CREATE TABLE `jogo` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `placar` int NOT NULL,
  `piorResultado` int DEFAULT NULL,
  `melhorResultado` int DEFAULT NULL,
  `mudouPior` int DEFAULT NULL,
  `mudouMelhor` int DEFAULT NULL,
  PRIMARY KEY (`Id`)
)

3)Quando iniciar o programa, o programa dará as seguintes opções:
  1- Cadastrar Resultados: Vai para o processo cadastrar os resultados, podendo cadastrar múltiplos
  2- Visualizar Resultados: Mostra todos os resultados cadastrados em uma tabela.
  9- Sair : Termina a aplicação
  Escolher qualquer outra opção que não as mencionadas acima dará uma mensagem de erro e pedirá para colocar outra opção.

4)Dentro do Cadastro de resultados, após informar o resultado, será perguntado se quer botar um novo resultado, com respostas S (sim) e N (não),
  tal como no menu inicial, qualquer resposta diferente das citadas dará uma mensagem de erro e pedirá para colocar uma resposta válida. 
