namespace Model

open CommandLine

type SequenceInfo = {
    Prefix : string
}

type MoveInfo = {
    Id : int
    Name : string
}

module Definitions =

    let (|Parsed|NotParsed|) (parserResult : ParserResult<_>) =

        match parserResult with
        | :? Parsed<_> -> Parsed
        | :? NotParsed<_> -> NotParsed
        | _ -> failwith "No debiéramos llegar aquí."
