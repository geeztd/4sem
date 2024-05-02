#include "Module.h"

void printSubset(const vector<int>& subset) {
	cout << "{ ";
	for (int num : subset) {
		cout << num << " ";
	}
	cout << "}\n";
}

void generateSubsets(const vector<int>& set, vector<int>& subset, int index) {

	if (index == set.size()) {
		printSubset(subset);
		return;
	}

	subset.push_back(set[index]);
	generateSubsets(set, subset, index + 1);

	subset.pop_back();
	generateSubsets(set, subset, index + 1);
}

void generateAllSubsets(const vector<int>& set) {
	vector<int> subset;
	generateSubsets(set, subset, 0);
}