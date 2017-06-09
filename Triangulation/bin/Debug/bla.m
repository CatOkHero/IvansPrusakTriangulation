clear;
points = dlmread('Points.txt');
triangles = dlmread('Triangles.txt');
solution = dlmread('Solution.txt');
X=points(:,1);
Y=points(:,2);
XX=Sort(X);
YY=Sort(Y);
ZZ=zeros(length(YY),length(XX));
for j=1:length(points)
    k=Find(points(j,:),XX,YY);
    ZZ(k(2),k(1))=solution(j);
end
surf(XX,YY,ZZ);
