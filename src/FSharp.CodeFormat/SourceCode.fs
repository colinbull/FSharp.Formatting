﻿// --------------------------------------------------------------------------------------
// F# CodeFormat (SourceCode.fs)
// (c) Tomas Petricek, 2012, Available under Apache 2.0 license.
// --------------------------------------------------------------------------------------
namespace FSharp.CodeFormat

// --------------------------------------------------------------------------------------
// Abstract Syntax representation of formatted source code
// --------------------------------------------------------------------------------------

/// A tool tip consists of a list of items reported from the compiler
type ToolTipSpans = list<ToolTipSpan>

/// A tool tip span can be emphasized text, plain text `Literal` or a line brak
and ToolTipSpan = 
  | Emphasis of ToolTipSpans
  | Literal of string
  | HardLineBreak

/// Classifies tokens reported by the F# lexer and F# PowerTools 
/// (PowerTools provide additional information e.g. whether a variable
/// is mutable, etc.)
[<RequireQualifiedAccess>]
type TokenKind = 
  | Keyword
  | String
  | Comment
  | Identifier
  | Inactive
  | Number
  | Operator
  | Preprocessor
  | TypeOrModule
  | Function
  | Pattern
  | MutableVar
  | Printf
  | Escaped
  | Default

/// Represents a kind of error reported from the F# compiler (warning or error)
[<RequireQualifiedAccess>]
type ErrorKind = 
  | Error
  | Warning

/// A token in a parsed F# code snippet. Aside from standard tokens reported from
/// the compiler (`Token`), this also includes `Error` (wrapping the underlined
/// tokens), `Omitted` for the special `[omit:...]` tags and `Output` for the special
/// `[output:...]` tag
type TokenSpan = 
  | Token of TokenKind * string * ToolTipSpans option
  | Error of ErrorKind * string * TokenSpans
  | Omitted of string * string
  | Output of string

/// A type alias representing a list of `TokenSpan` values
and TokenSpans = TokenSpan list

/// Represents a line of source code as a list of `TokenSpan` values. This is
/// a single case discriminated union with `Line` constructor.
type Line = Line of TokenSpans

/// An F# snippet consists of a snippet title and a list of lines
/// (wrapped in a single case discriminated union with `Snippet` constructor)
type Snippet = Snippet of string * Line list

/// Error reported from the F# compiler consists of location (start and end),
/// error kind and the message (wrapped in a single case discriminated union
/// with constructor `SourceError`)
type SourceError = SourceError of (int * int) * (int * int) * ErrorKind * string