using CM_3.IO;
using CM_3.Methods.LOS;
using CM_3.Methods.MCG;
using CM_3.Models;
using CM_3.Tools;

var sources = new SourcesForMatrix
{
    NFile = "kuslau4545.txt",
    IGFile = "ig4545.txt",
    JGFile = "jg4545.txt",
    GGFile = "gg4545.txt",
    DIFile = "di4545.txt"
};

var matrixI = new MatrixIO("../CM_3/Input/");

var sparseMatrix = new SparseMatrix();
matrixI.Read(sparseMatrix, sources);

//var hilbertGenerator = new HilbertGenerator();
//var hilbertSparseMatrix = new SparseMatrix();
//hilbertSparseMatrix.GenerateHilbert(matrixI, hilbertGenerator, "kuslau.txt");
//var xStar = hilbertGenerator.GenerateXStar(hilbertSparseMatrix.N);
//var pr = Calculator.MultiplyMatrixOnVector(hilbertSparseMatrix, xStar);

var vectorI = new VectorIO("../CM_3/Input/");
var vectorO = new VectorIO("../CM_3/Output/");

var x = new double[sparseMatrix.N];

var pr = vectorI.ReadDouble("pr4545.txt");
var start = vectorI.ReadDouble("start4545.txt");

var parametersI = new ParametersIO("../CM_3/Input/");

var tuple = parametersI.Read("kuslau4545.txt");
var maxIter = tuple.Item1;
var eps = tuple.Item2;

Array.Copy(start, x, sparseMatrix.N);
var msg = new MCG();
x = msg.Solve(sparseMatrix, x, pr, eps, maxIter);
vectorO.Write(x, "MCG.txt");

Array.Copy(start, x, sparseMatrix.N);
var los = new StandartLOS();
x = los.Solve(sparseMatrix, x, pr, eps, maxIter);
vectorO.Write(x, "LOS.txt");

Array.Copy(start, x, sparseMatrix.N);
var diagonalLOS = new DiagonalLOS();
x = diagonalLOS.Solve(sparseMatrix, x, pr, eps, maxIter);
vectorO.Write(x, "diagonalLOS.txt");

Array.Copy(start, x, sparseMatrix.N);
var choleskyLOS = new CholeskyLOS();
x = choleskyLOS.Solve(sparseMatrix, x, pr, eps, maxIter);
vectorO.Write(x, "choleskyLOS.txt");