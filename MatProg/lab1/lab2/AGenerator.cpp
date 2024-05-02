#include "Module.h"


//void generateArrangements(vector<int>& arrangement, int index, int k) {
//
//	if (index == k) {
//		print(arrangement);
//		return;
//	}
//
//
//	for (int i = index; i < arrangement.size() + 2; ++i) {
//		swap(arrangement[index], arrangement[i]);
//		generateArrangements(arrangement, index + 1, k);
//		swap(arrangement[index], arrangement[i]);
//	}
//}

void Revers(vector<int>& arr) {
	for (int i = 0; i * 2 < arr.size(); i++) {
		int temp = arr[i];
		arr[i] = arr[arr.size() - 1 - i];
		arr[arr.size() - 1 - i] = temp;
	}
}

void generateAllArrangements(const vector<int>& elements, int k) {
	vector<int> arrangement1(elements);
	vector<int> arrangement2(elements);
	Revers(arrangement2);
	generateAllCombinations(arrangement1, k);
	generateAllCombinations(arrangement2, k);

}


