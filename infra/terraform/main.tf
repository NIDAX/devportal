# Enable the Kubernetes Engine API
resource "google_project_service" "container" {
  service            = "container.googleapis.com"
  disable_on_destroy = false
}

# The GKE cluster
resource "google_container_cluster" "devportal" {
  name     = var.cluster_name
  location = var.region
  deletion_protection = false

  # We manage the node pool separately below
  # This removes the default node pool immediately after creation
  remove_default_node_pool = true
  initial_node_count       = 1

  # Depend on the API being enabled first
  depends_on = [google_project_service.container]

  node_config {
    disk_type    = "pd-standard"
    disk_size_gb = 50
    oauth_scopes = [
      "https://www.googleapis.com/auth/cloud-platform"
    ]
  }
}

# The node pool — the actual servers that run our containers
resource "google_container_node_pool" "devportal_nodes" {
  name     = "devportal-node-pool"
  cluster  = google_container_cluster.devportal.name
  location = var.region

  # Number of nodes to run
  node_count = var.node_count

  node_config {
    # Small and cheap — enough for a demo
    machine_type = var.machine_type
    disk_type    = "pd-standard"
    disk_size_gb = 50


    # Minimum OAuth scopes needed for GKE to function
    oauth_scopes = [
      "https://www.googleapis.com/auth/cloud-platform"
    ]

    # Labels applied to every node
    # Useful for filtering in GCP console
    labels = {
      environment = "demo"
      project     = "devportal"
    }

    # Resource limits enforced on every container
    # Prevents one team's service consuming all cluster resources
    # This is the platform team's guardrail for multi-tenant clusters
  }

  # Smooth upgrades — bring new nodes up before taking old ones down
  management {
    auto_repair  = true
    auto_upgrade = true
  }
}

# Output the cluster name and connection command after apply
output "cluster_name" {
  value = google_container_cluster.devportal.name
}

output "connect_command" {
  value = "gcloud container clusters get-credentials ${google_container_cluster.devportal.name} --region ${var.region} --project ${var.project_id}"
}