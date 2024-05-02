#include "Module.h"

const int NUM_SLOTS = 5;
const int NUM_CONTAINERS = 8;

const int MIN_CONTAINER_WEIGHT = 100;
const int MAX_CONTAINER_WEIGHT = 200;

const int MIN_PROFIT = 10;
const int MAX_PROFIT = 100;

const int MIN_SLOT_MIN_WEIGHT = 50;
const int MAX_SLOT_MIN_WEIGHT = 120;
const int MIN_SLOT_MAX_WEIGHT = 150;
const int MAX_SLOT_MAX_WEIGHT = 850;

class  Container
{
public:
	int id;
	int weight;
	int profit;
	Container(int w, int p, int i) {
		weight = w;
		profit = p;
		id = i;
	}
};
vector<int> minWeights(NUM_SLOTS, 0);
vector<int> maxWeights(NUM_SLOTS, 0);


int RandomGenerator(int from, int to) {
	return rand() % (to - from + 1) + from;
}

int CalculateTotalProfit(vector<Container>& containers) {
	int totalProfit = 0;
	for (const auto& container : containers) {
		totalProfit += container.profit;
	}
	return totalProfit;
}

int CalculateTotalWeight(vector<Container>& containers) {
	int totalWeight = 0;
	for (const auto& container : containers) {
		totalWeight += container.weight;
	}
	return totalWeight;
}

bool isValidPlacement(const vector<Container>& containers) {


	for (int i = 0; i < containers.size(); ++i) {
		if (containers[i].weight < minWeights[i % NUM_SLOTS] || containers[i].weight > maxWeights[i % NUM_SLOTS]) {
			return false;
		}
	}
	return true;
}
vector<Container> bestContainers;
int bestProfit = 0;

void generatePermutationsBoat(vector<Container>& permutation, int index) {
	if (index == permutation.size()) {
		if (isValidPlacement(permutation)) {
			int currentProfit = CalculateTotalProfit(permutation);
			if (currentProfit > bestProfit) {
				bestProfit = currentProfit;
				bestContainers = permutation;
			}
		}
		return;
	}

	for (int i = index; i < permutation.size(); ++i) {
		swap(permutation[index], permutation[i]);
		generatePermutationsBoat(permutation, index + 1);
		swap(permutation[index], permutation[i]);
	}
}



vector<Container> optimizeLoading(int num_Containers) {

	int bestProfit = 0;

	vector<Container> containers;
	for (int i = 0; i < num_Containers; ++i) {
		int weight = RandomGenerator(MIN_CONTAINER_WEIGHT, MAX_CONTAINER_WEIGHT);
		int profit = RandomGenerator(MIN_PROFIT, MAX_PROFIT);
		containers.push_back(Container(weight, profit, i + 1));
	}
	for (int i = 0; i < containers.size(); i++) {
		cout << "Контейнер номер " << containers[i].id << " Вес " << containers[i].weight << " Профит " << containers[i].profit << endl;
	}
	cout << "------------------" << endl;

	for (int i = 0; i < NUM_SLOTS; ++i) {
		minWeights[i] = RandomGenerator(MIN_SLOT_MIN_WEIGHT, MAX_SLOT_MIN_WEIGHT);
		maxWeights[i] = RandomGenerator(MIN_SLOT_MAX_WEIGHT, MAX_SLOT_MAX_WEIGHT);
	}

	for (int i = 0; i < NUM_SLOTS; ++i) {
		cout << "Слоты на корабле" << endl;
		cout << "Слот " << i + 1 << " min " << minWeights[i] << " max " << maxWeights[i] << endl;
	}
	cout << "-----------------" << endl;

	generatePermutationsBoat(containers, 0);

	return bestContainers;
}



void GenereteOptimizeLoad(int num_Containers) {
	vector<Container> optimalContainers = optimizeLoading(num_Containers);
	cout << "Слотов на судне " << NUM_SLOTS << endl;
	cout << "Всего контейнеров " << NUM_CONTAINERS << endl;
	cout << "Оптимальная загрузка:\n";
	for (const auto& container : optimalContainers) {
		cout << "Контейнер " << container.id << " Вес: " << container.weight << " Прибыль: " << container.profit << std::endl;
	}
	cout << "Итоговый вес: " << CalculateTotalWeight(optimalContainers) << std::endl;
	cout << "Итоговая прибыль: " << CalculateTotalProfit(optimalContainers) << std::endl;
}
