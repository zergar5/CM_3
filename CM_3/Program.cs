using CM_3.IO;
using CM_3.Methods.LOS;
using CM_3.Methods.MCG;
using CM_3.Models;

var sources = new SourcesForMatrix
{
    NFile = "kuslau.txt",
    IGFile = "ig.txt",
    JGFile = "jg.txt",
    GGFile = "gg.txt",
    DIFile = "di.txt"
};

var matrixI = new MatrixIO("../CM_3/Input/");

var sparseMatrix = new SparseMatrix();
matrixI.Read(sparseMatrix, sources);

var vectorI = new VectorIO("../CM_3/Input/");
var vectorO = new VectorIO("../CM_3/Output/");

var x = new double[sparseMatrix.N];

var pr = vectorI.ReadDouble("pr.txt");
var start = vectorI.ReadDouble("startVector.txt");

var parametersI = new ParametersIO("../CM_3/Input/");

var tuple = parametersI.Read("kuslau.txt");
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