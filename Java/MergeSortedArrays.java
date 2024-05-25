import java.util.Arrays;

public class MergeSortedArrays {
    public static void main(String[] args) {
        // Example sorted arrays
		//array1[10] = 1,2,4,6,9,10
        int[] array1 = {1,2,4,6,9,10};
        int[] array2 = {3, 5,7,8};

        // Merge the two sorted arrays
        int[] mergedArray = mergeArrays(array1, array2);

        // Print the merged array
        System.out.println("Merged array: " + Arrays.toString(mergedArray));
    }

    public static int[] mergeArrays(int[] array1, int[] array2) {
        int m = array1.length;
        int n = array2.length;
        int[] mergedArray = new int[m + n];
        
        int i = 0, j = 0, k = 0;

        // Merge the arrays into mergedArray
        while (i < m && j < n) {
            if (array1[i] <= array2[j]) {
                mergedArray[k++] = array1[i++];
            } else {
                mergedArray[k++] = array2[j++];
            }
        }

        // Copy remaining elements of array1, if any
        while (i < m) {
            mergedArray[k++] = array1[i++];
        }

        // Copy remaining elements of array2, if any
        while (j < n) {
            mergedArray[k++] = array2[j++];
        }

        // Copy the merged array back to array1
        for (i = 0; i < mergedArray.length; i++) {
            if (i < array1.length) {
                array1[i] = mergedArray[i];
            } else {
                // Expand array1 to fit the mergedArray
                array1 = Arrays.copyOf(array1, mergedArray.length);
                array1[i] = mergedArray[i];
            }
        }

        return array1;
    }
}