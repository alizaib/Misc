public class QuickSort {

    public static void main(String[] args) {
        // Example array
        int[] array = {10, 3, 2, 11, 1, 5};

        // Print the array before sorting
        System.out.println("Array before sorting: ");
        printArray(array);

        // Perform QuickSort on the array
        quickSort(array, 0, array.length - 1);

        // Print the array after sorting
        System.out.println("Array after sorting: ");
        printArray(array);
    }

    // Method to perform QuickSort
    public static void quickSort(int[] array, int low, int high) {
        if (low < high) {
            // Partition the array and get the pivot index
            int pi = partition(array, low, high);

            // Recursively sort the subarrays
            quickSort(array, low, pi - 1);
            quickSort(array, pi + 1, high);
        }
    }

    // Method to partition the array
    public static int partition(int[] array, int low, int high) {
        // Choose the rightmost element as pivot
        int pivot = array[high];

        // Pointer for the greater element
        int i = low - 1;

        // Traverse through all elements and
        // compare each element with the pivot
        for (int j = low; j < high; j++) {
            if (array[j] <= pivot) {
                i++;
                // Swap element at i with element at j
                int temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }
        }

        // Swap the pivot element with the element at i + 1
        int temp = array[i + 1];
        array[i + 1] = array[high];
        array[high] = temp;

        return i + 1; // Return the pivot index
    }

    // Method to print the array
    public static void printArray(int[] array) {
        for (int i : array) {
            System.out.print(i + " ");
        }
        System.out.println();
    }
}