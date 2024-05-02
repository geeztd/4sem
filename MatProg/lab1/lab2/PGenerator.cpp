#include "Module.h"

void generatePermutations(vector<int>& permutation, int index) {

	if (index == permutation.size()) {
		print(permutation);
		return;
	}


	for (int i = index; i < permutation.size(); ++i) {
		swap(permutation[index], permutation[i]);
		generatePermutations(permutation, index + 1);
		swap(permutation[index], permutation[i]);
	}
}

void generateAllPermutations(vector<int>& permutation) {
	generatePermutations(permutation, 0);
}


