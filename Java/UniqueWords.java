/* Q15. Given a string print the unique words of the string. */
import java.util.HashSet;
import java.util.Scanner;
import java.util.Set;

public class UniqueWords {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);

        // Input from the user
        System.out.println("Enter a string to find unique words:");
        String input = scanner.nextLine();

        // Find and print unique words
        Set<String> uniqueWords = getUniqueWords(input);
        System.out.println("Unique words in the string:");
        for (String word : uniqueWords) {
            System.out.println(word);
        }

        scanner.close();
    }

    // Method to get unique words from a given string
    public static Set<String> getUniqueWords(String input) {
        String[] words = input.split("\\s+");
        Set<String> uniqueWords = new HashSet<>();

        for (String word : words) {
            uniqueWords.add(word.toLowerCase());
        }

        return uniqueWords;
    }
}
