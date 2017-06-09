function [k ] = Find( point,X,Y )
%UNTITLED4 Summary of this function goes here
%   Detailed explanation goes here
x=point(1);
y=point(2);
k=zeros(1,2);
for i=1:length(X)
    if(x==X(i))
        k(1)=i;
        break;
    end
end
for i=1:length(Y)
    if(y==Y(i))
        k(2)=i;
        break;
    end
end
end

