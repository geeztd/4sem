#include "Module.h"


int main() {
	setlocale(0, "");
	srand(time(NULL));

	vector<int> arr = { 1, 2, 3, 4 };
	cout << "������������" << endl;
	generateAllSubsets(arr);

	int k = 2;
	cout << "���������" << endl;
	generateAllCombinations(arr, k);

	cout << "������������" << endl;
	generateAllPermutations(arr);

	cout << "����������" << endl;
	generateAllArrangements(arr, k);

	GenereteOptimizeLoad(8);

	clock_t  t1 = 0, t2 = 0;
	for (int i = 4; i <= 8; i++) {
		cout << i << " �����������" << endl;
		t1 = clock();
		GenereteOptimizeLoad(i);
		t2 = clock();
		cout << "���������������� " << t2 - t1 << endl;

	}


	return 0;
}
