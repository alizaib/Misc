/* Q19. WJP to display number of occurrence of all character*/
import java.util.HashMap;
import java.util.Map;
import java.util.Scanner;

public class CharacterFrequency {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);

        // Input from the user
        System.out.println("Enter a string to count character occurrences:");
        String input = scanner.nextLine();

        // Find and display the number of occurrences of each character
        displayCharacterOccurrences(input);

        scanner.close();
    }

    // Method to count and display the number of occurrences of each character in a given string
    public static void displayCharacterOccurrences(String input) {
        Map<Character, Integer> charCountMap = new HashMap<>();

        // Count the frequency of each character
        for (char ch : input.toCharArray()) {
            charCountMap.put(ch, charCountMap.getOrDefault(ch, 0) + 1);
        }

        // Display the characters and their counts
        System.out.println("Character occurrences in the string:");
        for (Map.Entry<Character, Integer> entry : charCountMap.entrySet()) {
            System.out.println(entry.getKey() + ": " + entry.getValue());
        }
    }
}
