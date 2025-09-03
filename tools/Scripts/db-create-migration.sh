MIGRATION_NAME = ${1:-"NewMigration"}
PROJECT = ${2:-"MillionLuxury.RealEstate.Infrastructure"}
STARTUP_PROJECT = ${3:-"MillionLuxury.RealEstate.API"}
OUTPUT_DIR = ${4:-"./Persistence/Migrations"}

dotnet ef migrations add $MIGRATION_NAME --project $PROJECT --startup-project $STARTUP_PROJECT --output-dir $OUTPUT_DIR