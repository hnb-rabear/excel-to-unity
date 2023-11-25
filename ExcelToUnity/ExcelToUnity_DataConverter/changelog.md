# Version 1.3
- Added the option to shrink all JSON sheets of an Excel file into a single JSON file.

# Version 1.3.1
- Improved user interface with responsive design.

# Version 1.3.2
- Minified JSON data type strings to a single line.

# Version 1.3.3
- Fixed empty cells to return a value of 0 instead of -1.
- Updated the condition for the Values array of Attributes. The Values array is now null if it has only one element equal to 0.

# Version 1.3.4
- Added an Unminimized Fields box.
- Updated the Help section.

# Version 1.3.5
- Added the option to export only IDs as Enums.

# Version 1.3.6
- Updated the JSON data structure to allow empty cells on the Field Row in the JSON Data Sheet.
- Added the [x] tag for JSON Column Rules. Columns with [x] in their name will be ignored during exportation.

# Version 1.3.7
- Fixed an issue where the last column with an empty name in the JSON Data Sheet would throw an exception.
- Added a comment column for the IDs and Constants Sheets.
- Added messages for keys with no values.
- Updated the example.

# Version 1.3.8
- Fixed responsive user interface issues.
- Rearranged functions for improved clarity.

# Version 1.3.9
- Removed the Merge Constants Sheets Checkbox and made merging sheets the default behavior.
- Added a Checkbox for exporting Constants, IDs, and Localizations Sheets separately.
- Added a Localizations Manager for managing multiple file localizations.

# Version 1.4.0
- Fixed issues with the Localization Text Component and referenced ID strings in the Constants Sheet.

# Version 1.4.1
- Removed license checking.
- Fixed default settings and updated documentation link.

# Version 1.4.2
- Fixed comments for enum type values in the IDs Sheet.

# Version 1.4.3
- Added a localization output folder and updated localization templates.

# Version 1.4.4
- Added a language characters map generator.

# Version 1.4.5
- Added method for getting system language.
- Updated Help

# Version 1.4.6
- Removed obsoleted features
- Updated localization templates
- Fixed IDx exporting of Single Excel