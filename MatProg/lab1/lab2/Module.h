#pragma once
#include <iostream>
#include <vector>
#include <algorithm>
#include <ctime>

using namespace std;

void generateAllSubsets(const vector<int>& set);
void generateAllCombinations(const vector<int>& elements, int k);
void generateAllArrangements(const vector<int>& elements, int k);
void generateAllPermutations(vector<int>& permutation);
void GenereteOptimizeLoad(int num_Containers);

void print(const vector<int>& arr);
