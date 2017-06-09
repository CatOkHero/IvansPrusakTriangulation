function [ y ] = isContain( X,point )
%UNTITLED Summary of this function goes here
%   Detailed explanation goes here
y=0;
for i=1:length(X)
    if(X(i)==point)
        y=1;
        break;
    end
end
end



