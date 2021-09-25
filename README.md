# One-million-cubes
 In this small project, one million cube entities are instantiated when the scene is loaded.
 
 The cube prefab is converted into an entity before being spawned in a simple grid formation with some separation.
 
 A job system is then used to make the cubes oscillate in a sinusoidal standing wave pattern. The speed of this is improved using the burst compiler.
 
 This project uses the ECS to improve the speed of the simulation.
