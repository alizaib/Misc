/* Q18. WJP to display duplicate character in string */
import java.util.HashMap;
import java.util.Map;
import java.util.Scanner;

public class DuplicateCharacters {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);

        // Input from the user
        System.out.println("Enter a string to find duplicate characters:");
        String input = scanner.nextLine();

        // Find and display duplicate characters
        findAndPrintDuplicateCharacters(input);

        scanner.close();
    }

    // Method to find and print duplicate characters in a given string
    public static void findAndPrintDuplicateCharacters(String input) {
        Map<Character, Integer> charCountMap = new HashMap<>();

        // Count the frequency of each character
        for (char ch : input.toCharArray()) {
            charCountMap.put(ch, charCountMap.getOrDefault(ch, 0) + 1);
        }

        // Display characters with a count greater than 1
        System.out.println("Duplicate characters in the string:");
        for (Map.Entry<Character, Integer> entry : charCountMap.entrySet()) {
            if (entry.getValue() > 1) {
                System.out.println(entry.getKey() + ": " + entry.getValue());
            }
        }
    }
}
