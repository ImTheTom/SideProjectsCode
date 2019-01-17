function [ meanOfValues ] = GetMeanSummer( data, startIndex, endIndex, state, type )
%GETMEANSUMMER Used to get the mean of a dataset
endValues = data(1:endIndex);
startValues = data(startIndex:end);
values = vertcat(endValues, startValues);
meanOfValues = mean(values);
fprintf('%s Mean %s in Summer: %d\n',state, type, round(meanOfValues));
end

