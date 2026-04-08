# Monopoly — C# Systems Architecture

A modular C# implementation of Monopoly built as a systems-focused programming exercise.  
The primary goal was to explore clean architecture, object-oriented design, and structured turn-based game logic — not UI or graphics.

-----

## Overview

The game runs as a console application and follows a structured turn pipeline:

1. Dice rolling
1. Player movement
1. Square resolution
1. Economic transactions
1. Jail and rule handling

-----

## Architecture

The project is organized into clearly separated core systems, making it easy to extend with additional rules, AI behaviours, or gameplay features.

```
Monopoly/
├── Core/
│   ├── Board/          # Board state, squares, property groups
│   ├── Players/        # Player state, inventory, turn management
│   ├── Managers/       # Game loop, transaction handling, rule enforcement
│   └── AI/             # AI decision-making logic
├── Test/               # Unit tests per system
└── Program.cs          # Entry point
```

-----

## Design Principles

- **Separation of concerns** — each system owns its domain with no cross-cutting dependencies
- **Extensibility** — new square types, rules, or AI strategies can be added without touching core logic
- **Testability** — systems are isolated and covered by unit tests in `Test/`
- **OOP patterns** — interfaces, inheritance, and composition used throughout

-----

## Getting Started

**Requirements:** .NET 6.0 or later

```bash
git clone https://github.com/Mohjo13/Monopoly.git
cd Monopoly
dotnet run
```

To run tests:

```bash
dotnet test
```

-----

## Purpose

This repository demonstrates backend game architecture and C# design principles in a turn-based simulation context. It was built as part of my game development education at Södertörn University, Stockholm.

-----

*Part of [mohjo13.github.io](https://mohjo13.github.io) — Game Developer & Designer portfolio*