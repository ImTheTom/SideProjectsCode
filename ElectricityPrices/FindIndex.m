function [ index ] = FindIndex( date, dates )
%FINDINDEX Used to find the index of a date
inDateFormat = datetime( date,'InputFormat','dd-MM-yyyy HH:mm:ss');
index = datefind(inDateFormat, dates);
end