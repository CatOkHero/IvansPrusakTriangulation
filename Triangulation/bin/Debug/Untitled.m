function S = Untitled(u,p,U,C)   
uspan = findKnotSpan(u,p,U);    
Nu = getBasisFuncs(u,p,U,uspan);   
S = zeros(1,numel(C(1,:)));     
for i=0:p        
    index = uspan-p+i;    
    S = S + C(index+1,:) * Nu(i+1);  
end
end

