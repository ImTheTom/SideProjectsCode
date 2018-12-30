function [ data ] = SetCategoricalValues( data, index )
%REMOVESTRINGVALUES sets categorical values to a integer
    categoricalValues = unique(data{:,index});
    integerValues = zeros(height(data),1);
    for i=1:height(data)
        integerValues(i) = find(strcmp(categoricalValues, data{i, index}));
    end
    data{:,index} = num2cell(integerValues);
end

