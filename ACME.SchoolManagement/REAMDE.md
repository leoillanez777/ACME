# ACME School Management

Este proyecto es una Prueba de Concepto (PoC) para un sistema de gestión de cursos y estudiantes para la escuela ACME. El objetivo principal es demostrar la implementación de un modelo de dominio rico, junto con una capa de servicios y pruebas unitarias.

## Decisiones de diseño

1. **Modelo de dominio rico**: Se ha implementado un modelo de dominio rico, donde las entidades (`Student`, `Course`, `Registration`) no solo contienen datos, sino también comportamiento relacionado. Por ejemplo, la clase `Course` tiene un método estático `Create` que valida las fechas de inicio y fin del curso.

2. **Separación de responsabilidades**: Se han separado las responsabilidades en diferentes clases y proyectos. Por ejemplo, la clase `RegistrationService` se encarga de la lógica de negocio relacionada con las matrículas, mientras que las clases `CourseRepository` y `StudentRepository` se encargan de la persistencia (aunque en este caso, se implementan en memoria).

3. **Interfaces y abstracciones**: Se han utilizado interfaces como `IPaymentGateway` e `IRepository` para permitir la implementación de diferentes servicios de pago y repositorios en el futuro, sin afectar el código existente.

4. **Pruebas unitarias**: Se han implementado pruebas unitarias utilizando xUnit.net para garantizar el correcto funcionamiento de las clases y servicios.

5. **Excepciones personalizadas**: Se han creado excepciones personalizadas para manejar casos de error específicos del dominio, como `InvalidCourseDateRangeException`, `DuplicateStudentException`, etc.

## Consideraciones adicionales

1. **Persistencia**: En esta PoC, no se ha implementado una base de datos real. En su lugar, se utilizan repositorios en memoria (`CourseRepository`, `StudentRepository`) que mantienen las colecciones de objetos en memoria.

2. **Pagos**: El `FakePaymentGateway` es una implementación de prueba que simula el procesamiento de pagos de manera aleatoria. En una implementación real, se debería integrar con un proveedor de servicios de pago.

3. **Separación de capas**: Aunque se ha separado la lógica de negocio en diferentes clases y proyectos, no se ha implementado una separación completa de capas (por ejemplo, una capa de presentación o API).

4. **Validaciones adicionales**: Se podrían agregar validaciones adicionales en el modelo de dominio, como restricciones en los nombres de los cursos o estudiantes, entre otros.

5. **Manejo de transacciones**: En una implementación real con una base de datos, se deberían implementar transacciones para garantizar la consistencia de los datos.

## Pasos siguientes

Si el proyecto continúa, se podrían abordar los siguientes puntos:

1. Integrar una base de datos real (por ejemplo, SQL Server, PostgreSQL, MongoDB) y reemplazar los repositorios en memoria por implementaciones que utilicen la base de datos.

2. Implementar una capa de presentación o API para exponer la funcionalidad del sistema a los usuarios finales.

3. Integrar con un proveedor de servicios de pago real.

4. Agregar funcionalidades adicionales, como gestión de profesores, calificaciones, horarios, etc.

5. Mejorar la seguridad y el manejo de autenticación y autorización.

6. Implementar patrones de diseño adicionales según sea necesario (por ejemplo, patrones de repositorio, Unidad de Trabajo, etc.).

7. Agregar pruebas de integración y pruebas de extremo a extremo.

8. Implementar prácticas de DevOps, como integración continua, entrega continua y despliegue continuo.

Espero que esta PoC y las pruebas unitarias sean suficientes para demostrar las capacidades básicas del sistema y servir como base para futuras iteraciones y mejoras.

```
Tiempo Invertido:
Inicio: 9am --> 6 Junio
Fin: 3pm --> 6 de Junio
```
Tiempo Total: **6 horas**