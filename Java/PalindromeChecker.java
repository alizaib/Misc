/* Q12. Write a program to check palindrome (MalayalaM) for both numbers and string? */
import java.util.Scanner;

public class PalindromeChecker {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);

        // Input from the user
        System.out.println("Enter a string or number to check for palindrome:");
        String input = scanner.nextLine();

        // Check if the input is a palindrome
        if (isPalindrome(input)) {
            System.out.println(input + " is a palindrome.");
        } else {
            System.out.println(input + " is not a palindrome.");
        }

        scanner.close();
    }

    // Method to check if a given string is a palindrome
    public static boolean isPalindrome(String input) {
        // Remove any non-alphanumeric characters and convert to lower case
        String cleanedInput = input.replaceAll("[^a-zA-Z0-9]", "").toLowerCase();

        int left = 0;
        int right = cleanedInput.length() - 1;

        while (left < right) {
            if (cleanedInput.charAt(left) != cleanedInput.charAt(right)) {
                return false;
            }
            left++;
            right--;
        }

        return true;
    }
}