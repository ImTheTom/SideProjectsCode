function [ error ] = CalculateError( data, dateArray, model, state, type, type2 )
%CalculateError Used to calculate the root mean square error 
error = 0;
for i=1:length(data)
    predicted = model.predict(dateArray(i,:));
    actual = data(i);
    error = error + sqrt((actual-predicted)^2);
end
error = error/length(data);

fprintf('%s %s error for %s is: %d\n',state, type, type2, round(error));
end