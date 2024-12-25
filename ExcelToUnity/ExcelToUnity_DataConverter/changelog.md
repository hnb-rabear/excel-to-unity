# Update Summaries

## Version 1.3
- Introduced the ability to condense all JSON sheets of an Excel file into a single JSON file.

## Version 1.3.1
- Enhanced the user interface with a responsive design.

## Version 1.3.2
- Compressed JSON data type strings into a single line.

## Version 1.3.3
- Adjusted empty cells to return a value of 0 instead of -1.
- Modified the condition for the Values array of Attributes to be null if it only contains one element equal to 0.

## Version 1.3.4
- Introduced an Unminimized Fields box.
- Updated the Help section.

## Version 1.3.5
- Provided the option to export only IDs as Enums.

## Version 1.3.6
- Updated the JSON data structure to accommodate empty cells on the Field Row in the JSON Data Sheet.
- Introduced the [x] tag for JSON Column Rules to ignore columns with [x] in their name during exportation.

## Version 1.3.7
- Resolved an issue where the last column with an empty name in the JSON Data Sheet would cause an exception.
- Added a comment column for the IDs and Constants Sheets.
- Included messages for keys without values.
- Updated the example.

## Version 1.3.8
- Addressed responsive user interface issues.
- Reorganized functions for better clarity.

## Version 1.3.9
- Removed the Merge Constants Sheets Checkbox and set merging sheets as the default behavior.
- Added a Checkbox for exporting Constants, IDs, and Localizations Sheets separately.
- Introduced a Localizations Manager for handling multiple file localizations.

## Version 1.4.0
- Resolved issues with the Localization Text Component and referenced ID strings in the Constants Sheet.

## Version 1.4.1
- Removed license checking.
- Corrected default settings and updated the documentation link.

## Version 1.4.2
- Fixed comments for enum type values in the IDs Sheet.

## Version 1.4.3
- Introduced a localization output folder and updated localization templates.

## Version 1.4.4
- Introduced a language characters map generator.

## Version 1.4.5
- Added a method for retrieving system language.
- Updated Help

## Version 1.4.6
- Removed obsolete features.
- Updated localization templates.
- Fixed IDs exporting in Single Excel feature.

## Version 1.4.7
- Removed Single Excel settings.
- Fixed LocalizationText Component.
- Fixed Separation Localization in Single Excel feature.

## Version 1.4.8
- Introduced a status column to verify the presence of an existing Excel file
- Resolved an issue with the filter functionality during Excel file addition
- Enhanced feature: now supports multiple nested classes/structs

## Version 1.4.9
- Implemented localization loading via Addressable Asset system, improving flexibility and resource management
- Refactored and cleaned up the codebase

## Version 1.5.0
- Supported Google Sheets

## Version 1.5.1
- Fixed localization template
- Updated example localization sheet
- Changed documentation link
- Renamed some settings

## Version 1.5.2
- Added conditions to bypass invalid spreadsheets.
- Fixed loading Localizations with Addressable Assets.
- Fixed output folder of Localization files.
- Renamed characters_map to characters_set

## Version 1.5.3
- Replaced loading localization with UniTask to use Task instead.
- Fixed issue with retrieving the localization folder.
- Fixed persistent fields not working
- Add character set that contain all languages

## Version 1.5.4
- Removed Help tab
- Renamed to SheetX and changed the Icon
- Fixed out-of-index error when removing a google sheet