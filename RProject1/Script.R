# Identificación de las películas vistas y no vistas por el usuario 329.
# Se asume que si la película no ha sido valorada es que no ha sido vista.


library(tidyverse)
library(dplyr)
library(gsheet)
library(recommenderlab)
library(purrr)


valoraciones = read.csv2(file = "Datos.csv", header = TRUE, sep = ";")
str(valoraciones)

valoraciones_tidy <- valoraciones %>% gather(key = "atractivo",
                                             value = "valoracion",
                                             - ï..usuario)
head(valoraciones_tidy)


usuarios_excluidos <- valoraciones_tidy %>% filter(!is.na(valoracion)) %>%
                      group_by(ï..usuario) %>% count() %>% filter(n < 2) 

str(usuarios_excluidos)


valoraciones_tidy2 <- valoraciones_tidy %>% filter(!ï..usuario %in% usuarios_excluidos)
str(valoraciones_tidy2)

# Se crea un dataframe en el que cada columna representa las valoraciones de 
# un usuario.
valoraciones_usuarios <- valoraciones_tidy %>%
                         spread(key = ï..usuario, value = valoracion, fill = NA)
valoraciones_usuarios2 <- valoraciones_tidy2 %>%
                         spread(key = ï..usuario, value = valoracion, fill = NA)
str(valoraciones_usuarios)
str(valoraciones_usuarios2[, -1])
# Función que calcula la similitud entre dos columnas
funcion_correlacion <- function(x, y) {
    correlacion <- cor(x, y, use = "na.or.complete", method = "pearson")
    return(correlacion)
}

# Se aplica la función de correlación a cada columna de valoraciones_usuarios,
# empelando como argumento "y" la columna del usuario "329"
similitud_usuarios <- map_dbl(.x = valoraciones_usuarios2[, -1],
                              .f = funcion_correlacion,
                              y = valoraciones_usuarios[, "5"])

similitud_usuarios <- data_frame(usuario = names(similitud_usuarios),
                                 similitud = similitud_usuarios) %>%
                                 arrange(desc(similitud))
head(similitud_usuarios)

# Identificación de las películas vistas y no vistas por el usuario 329.
# Se asume que si la película no ha sido valorada por el usuario 329 es que no
# ha sido vista.
atractivos_vistos <- valoraciones_tidy %>%
                    filter(ï..usuario == 5 & !is.na(valoracion)) %>%
                    pull(atractivo)

atractivos_no_vistos <- valoraciones_tidy %>%
                       filter(ï..usuario == 5 & is.na(valoracion)) %>%
                       pull(atractivo)

# Se inicia un bucle para predecir la valoración que el usuario 329 hará de cada
# una de las películas no vistas.

prediccion <- rep(NA, length(atractivos_no_vistos))
atractivo <- rep(NA, length(atractivos_no_vistos))
n_obs_prediccion <- rep(NA, length(atractivos_no_vistos))

for (i in seq_along(atractivos_no_vistos)) {
    # Usuarios que han visto la película i
    usuarios_atractivo_i <- valoraciones_tidy %>%
                         filter(atractivo == atractivos_no_vistos[i] &
                                !is.na(valoracion)) %>% pull(ï..usuario)
    # Si no hay un mínimo de usuarios que han visto la película, no se considera una
    # estimación suficientemente buena por lo que se pasa a la siguiente película.
    if (length(usuarios_atractivo_i) < 2) {
        next 
    }
    # Los 15 usuarios más parecidos de entre los que han visto la película i, cuya
    # similitud es >= 0.
    top_15_usuarios <- similitud_usuarios %>%
                     filter(similitud >= 0 & (usuario %in% usuarios_atractivo_i)) %>%
                     arrange(desc(similitud)) %>%
                     head(15)
    # Si no hay un mínimo de usuarios con valoraciones válidas, no se considera una
    # estimación suficientemente buena por lo que se pasa a la siguiente película.
    if (nrow(top_15_usuarios) < 2) {
        next 
    }

    # Valoraciones de esos 15 usuarios sobre la película i
    valoraciones_top_15 <- valoraciones_tidy %>%
                         filter(atractivo == atractivos_no_vistos[i] &
                                ï..usuario %in% top_15_usuarios$usuario)

    # Media ponderada de las valoraciones de los top_15_usuarios
    top_15_usuarios <- top_15_usuarios %>% left_join(valoraciones_top_15,
                                                   by = "ï..usuario")
    prediccion[i] <- sum(top_15_usuarios$similitud * top_15_usuarios$valoracion) /
                   sum(top_15_usuarios$similitud)
    atractivo[i] <- atractivos_no_vistos[i]
    n_obs_prediccion[i] <- nrow(top_15_usuarios)
}

top10_recomendaciones <- data.frame(atractivo, prediccion, n_obs_prediccion) %>%
                         arrange(desc(prediccion)) %>%
                         head(3)
top10_recomendaciones