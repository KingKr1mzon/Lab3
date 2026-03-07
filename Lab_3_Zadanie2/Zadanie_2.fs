(*
Все исходные данные вводятся с клавиатуры, организовать проверку вводимых значений.

Задание 2. Seq.fold
Решить задачу из лабораторной работы №2 (Задание 2. List.fold) для
последовательности
*)
open System

/// Универсальная функция для ввода целого числа с проверкой диапазона.
let rec readInt msg min max =
    printf "%s (от %d до %d): " msg min max
    let input = Console.ReadLine()
    match Int32.TryParse(input) with
    | true, value when value >= min && value <= max -> 
        value
    | _ ->
        printfn "Ошибка: введите число в диапазоне от %d до %d." min max
        readInt msg min max

/// Считывает последовательность строк (используем seq для ленивого или прямого ввода).
let readStrings count =
    printfn "Введите строки (по одной на каждой строке):"
    seq { for _ in 1 .. 1 .. count -> Console.ReadLine() }

/// Находит количество строк заданной длины, используя Seq.fold.
let countStrWithLenSeq targetLength strings =
    strings 
    |> Seq.fold (fun acc (str: string) -> 
        if str.Length = targetLength then acc + 1 else acc) 0

[<EntryPoint>]
let main _ =
    // 1. Ввод количества строк
    let count = readInt "Введите количество строк в последовательности" 1 100
    
    // 2. Ввод искомой длины
    let targetLen = readInt "Введите искомую длину строки" 0 500
    
    // 3. Создание последовательности строк
    let stringsSeq = readStrings count
    
    // 4. Подсчет через Seq.fold
    let resultCount = countStrWithLenSeq targetLen stringsSeq
    
    // 5. Вывод результата
    printfn "Количество строк длиной %i: %i" targetLen resultCount
    
    0