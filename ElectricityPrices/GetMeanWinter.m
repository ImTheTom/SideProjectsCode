function [ meanOfValues ] = GetMeanWinter(  data, startIndex, endIndex, state, type  )
%GETMEANWINTER Used to get the mean of winter indexs
values = data(startIndex:endIndex);
meanOfValues = mean(values);
fprintf('%s Mean %s in Winter: %d\n',state, type, round(meanOfValues));
end

