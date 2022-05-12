using AutoMapper;
using awsComprehend.Models;
using SistemaEncuestasBL.Data;
using SistemaEncuestasBL.Models;
using SistemaEncuestasBL.Repositories.Implements;
using SistemaEncuestasBL.Services.Implements;
using System.Collections.Generic;

namespace SistemaEncuestasBL.DTOs
{
    public class MapperConfig
    {

        public static MapperConfiguration MapperConfiguration()
        {
            return new MapperConfiguration(cfg =>
            {

                cfg.CreateMap<Encuesta, EncuestaDTO>().ConvertUsing<EncuestaAEncuestaDTOConverter>();
                cfg.CreateMap<EncuestaDTO, Encuesta>().ConvertUsing<EncuestaDTOAEncuestaConverter>();

                cfg.CreateMap<string, EstadoEncuesta>().ConvertUsing<StringAEstadoEncuestaConverter>();
                cfg.CreateMap<EstadoEncuesta, string>().ConvertUsing<EstadoEncuestaAStringConverter>();

                cfg.CreateMap<Pregunta, PreguntaDTO>();
                cfg.CreateMap<PreguntaDTO, Pregunta>().ConvertUsing<PreguntaDTOaPreguntaConverter>();

                cfg.CreateMap<string, TipoPregunta>().ConvertUsing<StringATipoPreguntoConverter>();
                cfg.CreateMap<TipoPregunta, string>().ConvertUsing<TipoPreguntaAStringConverter>();

                cfg.CreateMap<Persona, PersonaDTO>();
                cfg.CreateMap<PersonaDTO, Persona>();

                cfg.CreateMap<PreguntaTL, PreguntaTextoLibreDTO>();
                cfg.CreateMap<PreguntaTextoLibreDTO, PreguntaTL>();

                cfg.CreateMap<PreguntaOM, PreguntaOMDTO>();
                cfg.CreateMap<PreguntaOMDTO, PreguntaOM>();

                cfg.CreateMap<PreguntaOS, PreguntaOSDTO>();
                cfg.CreateMap<PreguntaOSDTO, PreguntaOS>();

                cfg.CreateMap<Opcion, OpcionDTO>();
                cfg.CreateMap<OpcionDTO, Opcion>();

                cfg.CreateMap<RespuestaTL, RespuestaTLDTO>();
                cfg.CreateMap<RespuestaTLDTO, RespuestaTL>();

                cfg.CreateMap<PreguntaOM, PreguntaOMDTO>();
                cfg.CreateMap<PreguntaOMDTO, PreguntaOM>();

                cfg.CreateMap<PreguntaDTO, PreguntaTL>();
                cfg.CreateMap<PreguntaDTO, PreguntaOM>();
                cfg.CreateMap<PreguntaDTO, PreguntaOS>();

                cfg.CreateMap<Respuesta, RespuestaDTO>();
                cfg.CreateMap<RespuestaDTO, Respuesta>();

                cfg.CreateMap<RespuestaDTO, RespuestaTL>();
                cfg.CreateMap<RespuestaDTO, RespuestaOM>();
                cfg.CreateMap<RespuestaDTO, RespuestaOS>();

                cfg.CreateMap<AwsComentario, RespuestaTL>().ConvertUsing<AwsComentarioARespuestaTLConverter>();
                cfg.CreateMap<RespuestaTL, AwsComentario>().ConvertUsing<RespuestaTLaAwsComentarioConverter>();

                cfg.CreateMap<Sentimiento, string>().ConvertUsing<SentimientoAStringConverter>();
                cfg.CreateMap<string, Sentimiento>().ConvertUsing<StringASentimientoConverter>();

                cfg.CreateMap<AwsFraseClave, FraseClave>();
                cfg.CreateMap<FraseClave, AwsFraseClave>();

                cfg.CreateMap<Resultado, ResultadoDTO>().ConvertUsing<ResultadoAResultadoDTOConverter>();
                cfg.CreateMap<ResultadoDTO, Resultado>().ConvertUsing<ResultadoDTOAResultadoConverter>();

                cfg.CreateMap<Usuario, UsuarioDTO>();
                cfg.CreateMap<UsuarioDTO, Usuario>();


            });

        }



        public class ResultadoAResultadoDTOConverter : ITypeConverter<Resultado, ResultadoDTO>
        {
            public ResultadoDTO Convert(Resultado source, ResultadoDTO destination, ResolutionContext context)
            {
                ResultadoDTO resultadoDTO = new ResultadoDTO();
                resultadoDTO.Texto = source.Texto;
                resultadoDTO.Valor = source.Total;
                resultadoDTO.Etiqueta = source.Etiqueta;
                return resultadoDTO;

            }
        }

        public class ResultadoDTOAResultadoConverter : ITypeConverter<ResultadoDTO, Resultado>
        {
            public Resultado Convert(ResultadoDTO source, Resultado destination, ResolutionContext context)
            {
                Resultado resultado = new Resultado();
                resultado.Texto = source.Texto;
                resultado.Total = source.Valor;
                resultado.Etiqueta = source.Etiqueta;
                return resultado;
            }
        }

        public class SentimientoAStringConverter : ITypeConverter<Sentimiento, string>
        {
            public string Convert(Sentimiento source, string destination, ResolutionContext context)
            {
                switch (source)
                {
                    case Sentimiento.MIXTO:
                        return "MIXED";

                    case Sentimiento.NEGATIVO:
                        return "NEGATIVE";


                    case Sentimiento.NEUTRO:
                        return "NEUTRAL";

                    case Sentimiento.POSITIVO:
                        return "POSITIVE";


                    default:
                        return "";
                }


            }
        }

        public class StringASentimientoConverter : ITypeConverter<string, Sentimiento>
        {
            public Sentimiento Convert(string source, Sentimiento destination, ResolutionContext context)
            {
                switch (source)
                {
                    case "MIXED":
                        return Sentimiento.MIXTO;

                    case "NEGATIVE":
                        return Sentimiento.NEGATIVO;


                    case "NEUTRAL":
                        return Sentimiento.NEUTRO;

                    case "POSITIVE":
                        return Sentimiento.POSITIVO;


                    default:
                        return Sentimiento.NEUTRO;
                }


            }
        }

        public class TipoPreguntaAStringConverter : ITypeConverter<TipoPregunta, string>
        {
            public string Convert(TipoPregunta source, string destination, ResolutionContext context)
            {
                switch (source)
                {
                    case TipoPregunta.TEXTOLIBRE:
                        return "TEXTOLIBRE";

                    case TipoPregunta.OPCIONMULTIPLE:
                        return "OPCIONMULTIPLE";

                    case TipoPregunta.OPCIONSIMPLE:
                        return "OPCIONSIMPLE";

                    default:
                        return "OPCIONSIMPLE"; ;
                }
            }
        }
        public class StringATipoPreguntoConverter : ITypeConverter<string, TipoPregunta>
        {
            public TipoPregunta Convert(string source, TipoPregunta destination, ResolutionContext context)
            {

                switch (source)
                {
                    case "TEXTOLIBRE":
                        return TipoPregunta.TEXTOLIBRE;

                    case "OPCIONMULTIPLE":
                        return TipoPregunta.OPCIONMULTIPLE;

                    case "OPCIONSIMPLE":
                        return TipoPregunta.OPCIONSIMPLE;

                    default:
                        return TipoPregunta.OPCIONSIMPLE;
                }
            }
        }


        public class EstadoEncuestaAStringConverter : ITypeConverter<EstadoEncuesta, string>
        {
            public string Convert(EstadoEncuesta source, string destination, ResolutionContext context)
            {
                switch (source)
                {
                    case EstadoEncuesta.Borrador:
                        return "BORRADOR";

                    case EstadoEncuesta.Publicada:
                        return "PUBLICADA";

                    case EstadoEncuesta.NoPublicada:
                        return "NOPUBLICADA";

                    default:
                        return "BORRADOR"; ;
                }
            }
        }
        public class StringAEstadoEncuestaConverter : ITypeConverter<string, EstadoEncuesta>
        {
            public EstadoEncuesta Convert(string source, EstadoEncuesta destination, ResolutionContext context)
            {

                switch (source)
                {
                    case "BORRADOR":
                        return EstadoEncuesta.Borrador;

                    case "PUBLICADA":
                        return EstadoEncuesta.Publicada;

                    case "NOPUBLICADA":
                        return EstadoEncuesta.NoPublicada;

                    default:
                        return EstadoEncuesta.Borrador;
                }
            }
        }

        public class EncuestaAEncuestaDTOConverter : ITypeConverter<Encuesta, EncuestaDTO>
        {

            //Encuesta -> EncuestaDTO
            public EncuestaDTO Convert(Encuesta source, EncuestaDTO destination, ResolutionContext context)
            {
                EncuestaDTO encuestaDTO = new EncuestaDTO();
                encuestaDTO.Denominacion = source.Denominacion;
                encuestaDTO.EncuestaID = source.EncuestaID;

                //EstadoEncuestaAStringConverter estadoConverter = new EstadoEncuestaAStringConverter();
                encuestaDTO.Estado = (new EstadoEncuestaAStringConverter()).Convert(source.Estado, encuestaDTO.Estado, context);

                encuestaDTO.FechaInicio = source.FechaInicio;
                encuestaDTO.FechaFin = source.FechaFin;
                encuestaDTO.CantidadEncuestados = source.CantidadEncuestados;
                encuestaDTO.Objetivo = source.Objetivo;

                ICollection<PreguntaDTO> preguntasDTO = new List<PreguntaDTO>();

                foreach (Pregunta p in source.Preguntas)
                {
                    preguntasDTO.Add((new PreguntaAPreguntaDTOConverter().Convert(p, new PreguntaDTO(), context)));
                }

                encuestaDTO.Preguntas = preguntasDTO;




                return encuestaDTO;
            }
        }

        public class EncuestaDTOAEncuestaConverter : ITypeConverter<EncuestaDTO, Encuesta>
        {
            //EncuestaDTO -> Encuesta
            public Encuesta Convert(EncuestaDTO source, Encuesta destination, ResolutionContext context)
            {


                Encuesta encuesta = new Encuesta
                {
                    Denominacion = source.Denominacion,
                    FechaInicio = source.FechaInicio,
                    FechaFin = source.FechaFin,
                    Objetivo = source.Objetivo
                };

                encuesta.EncuestaID = source.EncuestaID;

                encuesta.Estado = (new StringAEstadoEncuestaConverter()).Convert(source.Estado, encuesta.Estado, context); ;

                List<Pregunta> preguntas = new List<Pregunta>();

                foreach (PreguntaDTO pDto in source.Preguntas)
                {
                    preguntas.Add((new PreguntaDTOaPreguntaConverter().Convert(pDto, null, context)));
                }
                encuesta.Preguntas = preguntas;
                return encuesta;
            }
        }


        public class PreguntaAPreguntaDTOConverter : ITypeConverter<Pregunta, PreguntaDTO>
        {
            public PreguntaDTO Convert(Pregunta source, PreguntaDTO destination, ResolutionContext context)
            {
                PreguntaDTO respuesta = new PreguntaDTO();

                respuesta.EncuestaID = source.EncuestaID;
                respuesta.Orden = source.Orden;
                respuesta.PreguntaID = source.PreguntaID;
                respuesta.Requerida = source.Requerida;
                respuesta.TextoPregunta = source.TextoPregunta;

                TipoPreguntaAStringConverter conv = new TipoPreguntaAStringConverter();
                respuesta.Tipo = conv.Convert(source.Tipo, respuesta.Tipo, context);


                switch (source.Tipo)
                {
                    case TipoPregunta.OPCIONMULTIPLE:
                        PreguntaOM pOm = (PreguntaOM)source;
                        respuesta.Opciones = new List<OpcionDTO>();
                        foreach (Opcion o in pOm.Opciones)
                        {

                            respuesta.Opciones.Add(new OpcionDTO(o.OpcionID, o.OpcionTexto));

                        }

                        break;

                    case TipoPregunta.OPCIONSIMPLE:
                        PreguntaOS pOs = (PreguntaOS)source;
                        respuesta.Opciones = new List<OpcionDTO>();
                        foreach (Opcion o in pOs.Opciones)
                        {

                            respuesta.Opciones.Add(new OpcionDTO(o.OpcionID, o.OpcionTexto));

                        }

                        break;

                }


                return respuesta;

            }
        }


        public class PreguntaDTOaPreguntaConverter : ITypeConverter<PreguntaDTO, Pregunta>
        {
            public Pregunta Convert(PreguntaDTO source, Pregunta destination, ResolutionContext context)
            {


                Pregunta retorno = null;


                switch (source.Tipo)
                {
                    case "TEXTOLIBRE":

                        PreguntaTL pregTL = new PreguntaTL();
                        pregTL.Tipo = TipoPregunta.TEXTOLIBRE;

                        retorno = (Pregunta)pregTL;

                        break;

                    case "OPCIONMULTIPLE":
                        PreguntaOM pregOM = new PreguntaOM();
                        pregOM.Tipo = TipoPregunta.OPCIONMULTIPLE;
                        pregOM.Opciones = new List<Opcion>();
                        foreach (OpcionDTO oDto in source.Opciones)
                        {

                            pregOM.Opciones.Add(new Opcion(oDto.OpcionID, oDto.OpcionTexto));

                        }
                        retorno = (Pregunta)pregOM;

                        break;

                    case "OPCIONSIMPLE":
                        PreguntaOS pregOS = new PreguntaOS();
                        pregOS.Opciones = new List<Opcion>();
                        pregOS.Tipo = TipoPregunta.OPCIONSIMPLE;
                        foreach (OpcionDTO oDto in source.Opciones)
                        {

                            pregOS.Opciones.Add(new Opcion(oDto.OpcionID, oDto.OpcionTexto));

                        }
                        retorno = (Pregunta)pregOS;

                        break;

                }

                retorno.EncuestaID = source.EncuestaID;
                retorno.Orden = source.Orden;
                retorno.PreguntaID = source.PreguntaID;
                retorno.Requerida = source.Requerida;
                retorno.TextoPregunta = source.TextoPregunta;

                return retorno;
            }
        }


        public class RespuestaTLaAwsComentarioConverter : ITypeConverter<RespuestaTL, AwsComentario>
        {
            public AwsComentario Convert(RespuestaTL source, AwsComentario destination, ResolutionContext context)
            {
                AwsComentario comentario = new AwsComentario("");

                comentario.IdAuxiliar = source.RespuestaID;
                comentario.Idioma = source.Idioma;
                comentario.PuntajeMixto = source.Mixto;
                comentario.PuntajeNegativo = source.Negativo;
                comentario.PuntajeNeutral = source.Negativo;
                comentario.PuntajePositivo = source.Positivo;

                SentimientoAStringConverter conv = new SentimientoAStringConverter();
                //comentario.Sentimiento = conv.Convert(source.Sentimiento, destination.Sentimiento , context);

                comentario.Texto = source.TextoRespuesta;

                comentario.FrasesClave = null;

                return comentario;
            }
        }
        public class AwsComentarioARespuestaTLConverter : ITypeConverter<AwsComentario, RespuestaTL>
        {
            public RespuestaTL Convert(AwsComentario source, RespuestaTL destination, ResolutionContext context)
            {
                RespuestaTL respuesta = new RespuestaTL();
                respuesta.FrasesClave = new List<FraseClave>();
                respuesta.Idioma = source.Idioma;
                respuesta.Mixto = source.PuntajeMixto;
                respuesta.Negativo = source.PuntajeNegativo;
                respuesta.Neutral = source.PuntajeNeutral;
                respuesta.Positivo = source.PuntajePositivo;
                respuesta.RespuestaID = source.IdAuxiliar;

                StringASentimientoConverter conv = new StringASentimientoConverter();
                respuesta.Sentimiento = conv.Convert(source.Sentimiento, respuesta.Sentimiento, context);

                //destination.TextoRespuesta // No debería cambiarlo
                //destination.FrasesClave
                foreach (AwsFraseClave awsFc in source.FrasesClave)
                {
                    FraseClave fc = new FraseClave();
                    fc.Frase = awsFc.Frase;
                    fc.RespuestaID = respuesta.RespuestaID;
                    respuesta.FrasesClave.Add(fc);
                }

                return respuesta;
            }
        }


    }
}