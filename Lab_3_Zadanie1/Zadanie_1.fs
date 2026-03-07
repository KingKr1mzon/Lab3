(*
Все исходные данные вводятся с клавиатуры, организовать проверку вводимых значений.
Задание 1. Seq.map
Решить задачу из лабораторной работы №2 (Задание 1. List.map) для
последовательности
*)

open System

/// Проверка диапазона.
let rec readCount min max =
    printf "Введите количество значений (от %d до %d): " min max
    let input = Console.ReadLine()
    match Int32.TryParse(input) with
    | true, count when count >= min && count <= max -> 
        count
    | _ ->
        printfn "Ошибка: введите целое число в диапазоне от %d до %d." min max
        readCount min max
        
/// Считывание значений последовательности заданного размера с клавиатуры. (генератор списка)
let readStrings count =
    printfn "Введите значения: "
    seq { 
        for _ in 1 .. 1 .. count do 
            Console.ReadLine() 
    }

/// Считывает ровно один символ с клавиатуры.
let rec readPrefix () =
    printf "Введите один символ для добавления: "
    let input = Console.ReadLine()
    if not (String.IsNullOrEmpty(input)) && input.Length = 1 then
        input
    else
        printfn "Ошибка: необходимо ввести ровно один символ!"
        readPrefix ()

/// Добавляет префикс к каждому значению в списке.
let prependCharToAll prefix strings =
    // Используем функцию Seq.map и пайплайн |>
    strings |> Seq.map (fun item -> $"{prefix}{item}")

[<EntryPoint>]
let main _ =
    // Ввод и проверка количества значений последовательности.
    let count = readCount 1 6
    
    // Ввод префикса.
    let prefix = readPrefix ()
    
    // Ввод значений.
    let originalStrings = readStrings count
    
    // Формирование новой последовательности.
    let resultStrings = prependCharToAll prefix originalStrings
    
    printfn "Резуальтат: %A" (resultStrings |> Seq.toList)
    
    0