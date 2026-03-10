# Shotgun Hellcat

**Соло-проект аркадного бумер-шутера** в стилистике Франции XIII века.  
Вдохновение: Quake 1, DOOM Eternal, Blood.

**Статус:** активно разрабатывается (март 2026).  
**Цель:** демо 20–30 минут для портфолио.

## 🎮 Что уже сделано

- **Полноценная Quake-like система движения** на CharacterController
  - GroundedState / AirborneState (Finite State Machine)  
  - Accelerate, Friction, ApplyGravity (методы PlayerStateHelper, в точности повторяющие Quake 1)  
- **Zenject + основная архитектура для игрока** (DI, SO-конфиги, FSM + StateTransitionTable)
- **New Input System** + InputReader
- **Подготовка под ключевые механики** (dash + hook на одну кнопку Shift, glory kill)

## 🛠 Технологии

- Unity 6 + HDRP
- Zenject
- New Input System
- CharacterController + полностью кастомная физика

## Что в процессе (март-апрель 2026)

1. Полный рефакторинг камеры + удаление legacy FirstPersonCameraController
2. Dash/Hook
3. Базовое оружие (hitscan + weapon wheel)
4. Первый враг

## Как запустить

1. Скачать репозиторий
2. Открыть сцену `TestScene` (единственная рабочая)
3. Play

(пока только движение — оружие и враги в работе)
