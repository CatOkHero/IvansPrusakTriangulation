function [ y ] = basisFunction( triangle, points, point )


xi = points(triangle(1),:);
xj = points(triangle(2),:);
xm = points(triangle(3),:);

ai = xj(1) * xm(2) - xj(2) * xm(1);
aj = xm(1) * xi(2) - xm(2) * xi(1);
am = xi(1) * xj(2) - xi(2) * xj(1);

bi = xj(2) - xm(2);
bj = xm(2) - xi(2);
bm = xi(2) - xj(2);

ci = xm(1) - xj(1);
cj = xi(1) - xm(1);
cm = xj(1) - xi(1);


if(isequal(point,xi) || isequal(point,xj) || isequal(point,xm))
    y = [ai+bi*point(1)+ci*point(2),aj+bj*point(1)+cj*point(2),am+bm*point(1)+cm*point(2)];
else
    y=[0 0 0];
end;
end

