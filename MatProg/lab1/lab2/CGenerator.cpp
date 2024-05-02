#include "Module.h"

void print(const vector<int>& arr) {
	for (int num : arr) {
		cout << num << " ";
	}
	cout << endl;
}

void generateCombinations(const vector<int>& elements, vector<int>& combination, int index, int k) {

	if (combination.size() == k) {
		print(combination);
		return;
	}

	for (int i = index; i < elements.size(); ++i) {
		combination.push_back(elements[i]);
		generateCombinations(elements, combination, i + 1, k);
		combination.pop_back();
	}
}

void generateAllCombinations(const vector<int>& elements, int k) {
	vector<int> combination;
	generateCombinations(elements, combination, 0, k);
}


