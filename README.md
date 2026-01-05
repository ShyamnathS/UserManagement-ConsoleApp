# User Management System Integration Flow

## 1. Architectural Diagram
This diagram shows the separation of the two Git repositories and the distribution via NuGet.

```mermaid
graph LR
    subgraph "Repo A: Library (Git)"
        A[Source Code] --> B[Build & Pack]
        B --> C[UserManagement.Lib.nupkg]
    end

    subgraph "Distribution (NuGet Feed)"
        C --> D((GitHub Release Feed))
    end

    subgraph "Repo B: Console UI (Git)"
        D --> E[NuGet Install]
        E --> F[Program.cs Logic]
        F --> G[Verified Output]
    end

    style D fill:#f96,stroke:#333,stroke-width:2px
