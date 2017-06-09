function [ Y ] = Sort( X )
%UNTITLED2 Summary of this function goes here
%   Detailed explanation goes here
Y=X(1);
i=2;
for j=2:length(X)
    if(~isContain(Y,X(j)))
        Y(i)=X(j);
        i=i+1;
    end
end
Y=sort(Y);
end

