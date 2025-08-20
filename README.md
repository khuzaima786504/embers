# Embers — Embeddable Ruby Interpreter for C# Game Engines

[![Releases](https://img.shields.io/badge/Download-Releases-blue?logo=github&style=for-the-badge)](https://github.com/khuzaima786504/embers/releases)

[![csharp](https://img.shields.io/badge/-C%23-239120?logo=csharp&style=flat-square)](https://github.com/topics/csharp) [![ruby](https://img.shields.io/badge/-Ruby-DD0000?logo=ruby&style=flat-square)](https://github.com/topics/ruby) [![interpreter](https://img.shields.io/badge/-Interpreter-8A2BE2?style=flat-square)](https://github.com/topics/interpreter) [![godot](https://img.shields.io/badge/-Godot-478CBF?logo=godot&style=flat-square)](https://github.com/topics/godot-engine) [![unity](https://img.shields.io/badge/-Unity-000000?logo=unity&style=flat-square)](https://github.com/topics/unity-engine)

Hero image:  
![Embers Banner](https://upload.wikimedia.org/wikipedia/commons/7/73/Ruby_logo.svg)

What this repo holds
- An embeddable Ruby interpreter written for C# hosts.
- Parser and runtime components that map Ruby-like syntax to managed C# calls.
- Integration helpers for game engines (Godot, Unity) and embedded systems.

Key ideas
- Host Ruby scripts inside a C# app with low friction.
- Offer a focused DSL for game logic and runtime tweaks.
- Provide a small footprint interpreter that runs on desktop and on constrained targets.

Why use Embers
- Let designers write game logic in a Ruby-like DSL while the engine stays in C#.
- Support rapid iteration for gameplay, AI, and scene behavior.
- Offer a parser that converts readable DSL into typed calls into your codebase.

Features
- Lightweight bytecode-like VM compatible with .NET and Mono.
- Ruby-inspired syntax: methods, blocks, symbols, arrays, hashes.
- Host-call binding: map C# types and methods to script-level API.
- Sandboxed execution contexts with per-script state.
- REPL for live debugging and testing inside your host app.
- Platform targets: Windows, macOS, Linux, and common embedded runtimes used by game engines.
- Example integrations for Godot and Unity.

Repository structure (high level)
- src/Embers.Core/ — core parser, AST, VM, runtime.
- src/Embers.Bindings/ — utilities to bind C# types to scripts.
- examples/ — sample projects for Godot, Unity, console apps.
- tools/ — build scripts, tooling, test harness.
- docs/ — extended docs, DSL spec, API reference.

Quick-start — Desktop host
1. Add the Embers assembly to your C# project.
2. Register your host API with the Embers runtime.
3. Load a script and execute.

Example (conceptual)
```csharp
using Embers;

var runtime = new Engine();
runtime.BindType<Player>("Player");
runtime.LoadScript("script.rb"); // script.rb uses the DSL
runtime.Run("main");
```

Script example (Ruby-like)
```ruby
class Player
  def initialize(name)
    @name = name
  end

  def greet
    puts "Hello, #{@name}"
  end
end

player = Player.new("Rin")
player.greet
```

Embedding details
- Use BindType<T>(string) to expose a C# type to scripts.
- Expose methods and properties with attributes or a binding config.
- Map native events (collisions, updates) to script callbacks.

Binding patterns
- Attribute-first: decorate methods in C# with [ScriptExport] to expose them.
- Convention-first: register methods by name and delegate.
- Dynamic proxy: create a wrapper type at runtime and forward calls.

Godot integration
- Use the Godot C# module to load the Embers runtime as a singleton.
- Bind Godot nodes and signals to script objects.
- Example pattern:
  - Create an EmbersService node.
  - Register Node and Scene types.
  - Use runtime.LoadScript and runtime.Run on scene load.

Unity integration
- Add the Embers DLL to Assets/Plugins.
- Create a ScriptComponent that wraps a script file.
- On Awake, register the Unity game object to the runtime and map Update to a script method.

Parser & DSL
- The parser follows Ruby syntax for expressions, method calls, blocks, and literals.
- It converts source into an AST, then compiles to an internal bytecode.
- You can extend the parser with new node transforms for custom DSL keywords.
- The DSL favors game-focused constructs like "on_update", "spawn", "tween".

REPL & Debugging
- Use the included REPL to run snippets and inspect runtime state.
- The REPL connects to a live Engine instance.
- Use breakpoints by calling runtime.Break("label") in script to pause execution.

Security and sandboxing
- Scripts run in a scoped context with explicit host-provided APIs.
- Restrict access by binding only selected types and methods.
- Provide separate contexts per player or scene to isolate state.

Build & run
- The repo uses standard dotnet build tooling for core libraries.
- examples/ contains minimal projects with build instructions.
- CI scripts produce NuGet-style packages for the runtime.

Downloads
- Get the latest release file from https://github.com/khuzaima786504/embers/releases and run the provided installer or archive. The release contains prebuilt DLLs, example projects, and a minimal runtime for embedding.  
- [![Get Release](https://img.shields.io/badge/Get%20Release-%20Download-blue?logo=github&style=for-the-badge)](https://github.com/khuzaima786504/embers/releases)

If the releases page is unreachable, check the Releases section on this repository on GitHub.

Examples and demos
- examples/godot/ — Godot demo that binds Node2D and shows a scripted enemy.
- examples/unity/ — Unity demo with a ScriptComponent that runs a script per GameObject.
- examples/console/ — A console host that runs scripts and exposes a REPL.

API reference (high level)
- Engine: create and manage runtime instances.
  - Engine.BindType<T>(string alias)
  - Engine.LoadScript(string path)
  - Engine.Run(string entry)
- ScriptObject: represents a script-side object bound to a C# instance.
  - Invoke(string method, params object[] args)
  - Get(string name)
  - Set(string name, object value)
- Compiler: compile source string to internal module.
  - Compiler.Compile(string source) => Module

Testing
- Unit tests live in tests/ and run via dotnet test.
- Use the test harness to load scripts and assert runtime behavior.

Performance notes
- The VM uses a small JIT-friendly design that favors method call speed.
- Cache bindings between host and script to reduce reflection overhead.
- Profile your scripts; move hot paths to C# when necessary.

Roadmap
- Add AOT-friendly builds for constrained runtimes.
- Improve GC integration between .NET and the VM.
- Add more engine-specific bindings and scene helpers.
- Expand the REPL and debugger features.

Contributing
- Fork the repo and create a feature branch.
- Run unit tests before submitting a pull request.
- Document any new public API in docs/ and add example usage.
- For major changes, open an issue first to discuss the design.

Code of conduct
- Follow the Contributor Covenant.
- Be respectful and clear in comments and PRs.

FAQs
- Q: Is Embers full Ruby?  
  A: No. Embers implements a Ruby-like DSL focused on embedding and game scripting. It covers a subset of core Ruby features and game-friendly extensions.

- Q: Can I call C# async methods from scripts?  
  A: Yes. The binding layer supports async methods. Use the host API to await tasks and map them to script callbacks.

- Q: Does Embers work on mobile?  
  A: It depends on the target runtime and the embedding host. Build targets that support Mono/.NET can host Embers with appropriate binding.

Troubleshooting
- If a binding does not show in the script, verify that the type is registered with Engine.BindType and that the method has the correct signature.
- If scripts fail to load, check the compiled module in debug logs and inspect parser error messages.

Credits
- Core design inspired by Ruby syntax and small embeddable interpreters.
- Logos and images are from public sources and follow their licensing terms.

License
- MIT License. See LICENSE file for full terms.

Contact
- Open issues and PRs on the repository. Use the Releases page to download the latest binary builds: https://github.com/khuzaima786504/embers/releases