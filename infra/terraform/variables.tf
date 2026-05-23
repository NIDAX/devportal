variable "project_id" {
  description = "GCP project ID"
  type        = string
}

variable "region" {
  description = "GCP region to deploy resources"
  type        = string
  default     = "europe-west1-b"
}

variable "cluster_name" {
  description = "Name of the GKE cluster"
  type        = string
  default     = "devportal-cluster"
}

variable "node_count" {
  description = "Number of nodes in the cluster"
  type        = number
  default     = 1
}

variable "machine_type" {
  description = "GCP machine type for cluster nodes"
  type        = string
  default     = "e2-medium"
}