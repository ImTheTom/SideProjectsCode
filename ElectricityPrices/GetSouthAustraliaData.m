function [ saDate, saDemand, saPrice ] = GetSouthAustraliaData()
%GETSOUTHAUSTRALIADATE Used to get SA data
southAustraliaJan = readtable('SouthAustralia/2018/Jan2018.csv');
saJDate = southAustraliaJan.SETTLEMENTDATE;
saJDate=datetime( saJDate,'InputFormat','yyyy/MM/dd HH:mm:ss');
saJDemand = southAustraliaJan.TOTALDEMAND;
saJPrice = southAustraliaJan.RRP;

southAustralia = readtable('SouthAustralia/2018/total-no-jan.csv');
saDate = southAustralia.SETTLEMENTDATE;
saDate=datetime( saDate,'InputFormat','yyyy/MM/dd HH:mm:ss');
saDemand = southAustralia.TOTALDEMAND;
saPrice = southAustralia.RRP;

saDate = vertcat(saJDate, saDate);
saDemand = vertcat(saJDemand, saDemand);
saPrice = vertcat(saJPrice, saPrice);
end