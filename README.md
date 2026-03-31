# 🎮 Проект «Тысяча объектов» | Optimization Demo

> Демонстрация оптимизации производительности в Unity: от *тормозящего* кода до 60 FPS на слабых устройствах. Все видео с демонстрацией в папке Media.

[![Unity](https://img.shields.io/badge/Unity-2021.3+-000.svg?logo=unity)](https://unity.com)
[![DOTS](https://img.shields.io/badge/DOTS-Jobs%20%2B%20Burst-orange)](https://docs.unity3d.com/Packages/com.unity.jobs@latest)

---

## 🎯 Суть проекта

Показать разницу между наивным подходом и профессиональной оптимизацией:

| Версия | Технология | FPS | CPU (Scripts) |
|--------|------------|-----|---------------|
| ❌ **Плохо** | MonoBehaviour Update (1000×) | 20-40 | 15-25 мс |
| ✅ **Хорошо** | Jobs + Burst + GPU Instancing | 60+ | 1-3 мс |

---

## ⚙️ Требования

- **Unity:** 2021.3 LTS или новее
- **Пакеты:** `Jobs`, `Collections`, `Burst` (установить через Package Manager)
- **Материал:** включить галочку **☑ Enable GPU Instancing** в инспекторе

---

## 🚀 Быстрый старт

1. Откройте сцену `Scenes/DemoScene.unity`
2. Для **«плохой»** версии: активируйте `BadVersion_Manager`
3. Для **«хорошей»** версии: активируйте `GoodVersion_Manager`
4. Нажмите **▶ Play** и откройте **Profiler** (`Window → Analysis → Profiler`)

---

## 📊 Сравнение производительности

| Метрика | ❌ До | ✅ После |
|---------|-------|----------|
| **CPU: Scripts** | 15-25 мс | 1-3 мс |
| **Draw Calls** | ~1000 | 1-5 |
| **GC Alloc / кадр** | ~50 КБ | 0 Б |
| **FPS (PC)** | 20-40 | 60+ |
| **FPS (Android)** | 10-25 | 30-60 |

---
