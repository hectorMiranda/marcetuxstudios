# Bootstrap the storage account with az CLI before running terraform init.
# The lock is a blob lease — concurrent applies queue instead of racing.
terraform {
  backend "azurerm" {
    resource_group_name  = "rg-tfstate"
    storage_account_name = "sttfstatebank01"
    container_name       = "tfstate"
    key                  = "banking-infra/prod.tfstate"
  }
}

provider "azurerm" {
  features {}
}

resource "azurerm_resource_group" "main" {
  name     = "rg-banking-prod"
  location = "West US 2"
}
