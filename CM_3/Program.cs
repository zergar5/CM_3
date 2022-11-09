using CM_3.IO;
using CM_3.Methods.LOS;
using CM_3.Methods.MCG;
using CM_3.Models;
using CM_3.Tools;
using System.Globalization;
using CM_3.Tools.SolutionCheck;

var sources = new SourcesForMatrix
{
    NFile = "kuslau5.txt",
    IGFile = "ig5.txt",
    JGFile = "jg5.txt",
    GGFile = "gg5.txt",
    DIFile = "di5.txt"
};

var matrixI = new MatrixIO("../CM_3/Tests/");

var sparseMatrix = new SparseMatrix();
matrixI.Read(sparseMatrix, sources);

//var hilbertGenerator = new HilbertGenerator();

//var sparseMatrix = new SparseMatrix();
//sparseMatrix.GenerateHilbert(matrixI, hilbertGenerator, "kuslau.txt");

//var xStar = hilbertGenerator.GenerateXStar(sparseMatrix.N);
//var start = hilbertGenerator.GenerateXStart(sparseMatrix.N);
//var pr = Calculator.MultiplyMatrixOnVector(sparseMatrix, xStar);

var vectorI = new VectorIO("../CM_3/Tests/");
var vectorO = new VectorIO("../CM_3/Output/");

var x = new double[sparseMatrix.N];

var pr = vectorI.ReadDouble("pr5.txt");
var start = vectorI.ReadDouble("start5.txt");

var parametersI = new ParametersIO("../CM_3/Tests/");

var (maxIter, eps) = parametersI.ReadMethodParameters("kuslau5.txt");

var solutionChecker = new SolutionChecker();

Array.Copy(start, x, sparseMatrix.N);
var MCG = new MCG();
x = MCG.Solve(sparseMatrix, x, pr, eps, maxIter);
solutionChecker.CalcError(x, sparseMatrix.N);
vectorO.Write(x, "MCG.txt");

Array.Copy(start, x, sparseMatrix.N);
var LOS = new StandartLOS();
x = LOS.Solve(sparseMatrix, x, pr, eps, maxIter);
solutionChecker.CalcError(x, sparseMatrix.N);
vectorO.Write(x, "LOS.txt");

Array.Copy(start, x, sparseMatrix.N);
var diagonalLOS = new DiagonalLOS();
x = diagonalLOS.Solve(sparseMatrix, x, pr, eps, maxIter);
solutionChecker.CalcError(x, sparseMatrix.N);
vectorO.Write(x, "diagonalLOS.txt");

Array.Copy(start, x, sparseMatrix.N);
var choleskyLOS = new CholeskyLOS();
x = choleskyLOS.Solve(sparseMatrix, x, pr, eps, maxIter);
solutionChecker.CalcError(x, sparseMatrix.N);
vectorO.Write(x, "choleskyLOS.txt");