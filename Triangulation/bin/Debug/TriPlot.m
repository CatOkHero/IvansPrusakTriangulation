clear;
figure
points = dlmread('Points.txt');
solution = dlmread('Solution.txt');
X=points(:,1);
Y=points(:,2);
tri=delaunay(X,Y);
trisurf(tri,X,Y,solution);
