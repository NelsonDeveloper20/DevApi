

CREATE FUNCTION dbo.CalcularTela(@ParametroID INT)
RETURNS DECIMAL(18, 3)
AS
BEGIN
    DECLARE @Tela DECIMAL(18, 3);
SELECT @Tela = 

-- --TELA---- CONTINUAR DESDE AQUI MARISOL===============>  
case when   
 subString(dop.CodigoProducto,1,5)   ='PRTRS' then  
-- ROLLER SHADE   
(select  top 1  
case when dop1.Accionamiento='Manual' then  
  
   case when a.CantidadProducto=1 then   
   --  
          case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.025) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.025) -0.001)  
            when dop1.CodigoTubo='PALRS00000005'    then ((dop1.Ancho - 0.030) -0.001)  
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Ancho - 0.028) -0.001)  
            when dop1.CodigoTubo='PALLU00000003'    then ((dop1.Ancho - 0.030) -0.001)   
            -- when dop1.CodigoTubo='PALLU00000004'    then ((dop1.Ancho - 0.030) -0.001)   
          end  
 --  
        when isnull(a.CantidadProducto,'0')='0' then  
     --  
          case  when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.025) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.025) -0.001)  
            when dop1.CodigoTubo='PALRS00000005'    then ((dop1.Ancho - 0.030) -0.001)  
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Ancho - 0.028) -0.001)  
            when dop1.CodigoTubo='PALLU00000003'    then ((dop1.Ancho - 0.030) -0.001)   
            -- when dop1.CodigoTubo='PALLU00000004'    then ((dop1.Ancho - 0.030) -0.001)   
            end  
 --  
     when a.CantidadProducto=2 then  
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.023) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.023) -0.001)  
            when dop1.CodigoTubo='PALRS00000005'    then ((dop1.Ancho - 0.025) -0.001)  
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Ancho - 0.023) -0.001)  
            when dop1.CodigoTubo='PALLU00000003'    then ((dop1.Ancho - 0.025) -0.001)    
          end  
 --  
     when a.CantidadProducto=3 then  
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.021) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.021) -0.001)  
            when dop1.CodigoTubo='PALRS00000005'    then ((dop1.Ancho - 0.021) -0.001)  
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Ancho - 0.021) -0.001)   
          end  
 --  
     when a.CantidadProducto >=4 then   
      --  
           case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.021) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.021) -0.001)  
            when dop1.CodigoTubo='PALRS00000005'    then ((dop1.Ancho - 0.021) -0.001)  
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Ancho - 0.021) -0.001)   
          end   
   end   
     
  when dop1.Accionamiento='Motorizado' then   
 case when a.CantidadProducto=1 then   
     --  
          case when dop1.CodigoTubo='PALRS00000003'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.025) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.025) -0.001)  
            when dop1.CodigoTubo='PALRS00000005'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.030) -0.001)  
            when dop1.CodigoTubo='PALRZ00000026'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.028) -0.001)   
            when dop1.CodigoTubo='PALLU00000003'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.030) -0.001)   
              
              
            -- RAEX  
              
            when dop1.CodigoTubo='PALRS00000005'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then ((dop1.Ancho - 0.038) -0.001)               
            when dop1.CodigoTubo='PALRS00000024'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then ((dop1.Ancho - 0.038) -0.001)               
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then ((dop1.Ancho - 0.040) -0.001)   
            -- LUTRON  
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTLU00000002,MOTLU00000004,MOTLU00000003,MOTLU00000001')>0   then ((dop1.Ancho - 0.040) -0.001)   
              
          end  
 --  
      when isnull(a.CantidadProducto,'0')='0' then   
       --  
           case when dop1.CodigoTubo='PALRS00000003'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.025) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.025) -0.001)  
            when dop1.CodigoTubo='PALRS00000005'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.030) -0.001)  
            when dop1.CodigoTubo='PALRZ00000026'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.028) -0.001)   
            when dop1.CodigoTubo='PALLU00000003'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.030) -0.001)   
              
            when dop1.CodigoTubo='PALRS00000005'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then ((dop1.Ancho - 0.038) -0.001)               
            when dop1.CodigoTubo='PALRS00000024'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then ((dop1.Ancho - 0.038) -0.001)               
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then ((dop1.Ancho - 0.040) -0.001)    
            -- LUTRON  
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTLU00000002,MOTLU00000004,MOTLU00000003,MOTLU00000001')>0   then ((dop1.Ancho - 0.040) -0.001)   
              
          end  
 --  
   when a.CantidadProducto=2 then   
       --  
          case when dop1.CodigoTubo='PALRS00000003'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.023) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.023) -0.001)  
            when dop1.CodigoTubo='PALRS00000005'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.025) -0.001)  
            when dop1.CodigoTubo='PALRZ00000026'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.023) -0.001)   
            when dop1.CodigoTubo='PALLU00000003'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.025) -0.001)   
              
            -- RAEX  
            when dop1.CodigoTubo='PALRS00000005'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then ((dop1.Ancho - 0.029) -0.001)               
            when dop1.CodigoTubo='PALRS00000024'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then ((dop1.Ancho - 0.029) -0.001)               
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then ((dop1.Ancho - 0.040) -0.001)    
            -- LUTRON  
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTLU00000002,MOTLU00000004,MOTLU00000003,MOTLU00000001')>0   then ((dop1.Ancho - 0.040) -0.001)   
              
          end  
 --  
    when a.CantidadProducto=3 then   
       --  
          case when dop1.CodigoTubo='PALRS00000003'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.021) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.021) -0.001)  
            when dop1.CodigoTubo='PALRS00000005'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.021) -0.001)  
            when dop1.CodigoTubo='PALRZ00000026'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.021) -0.001)               
              
            -- RAEX   
            when dop1.CodigoTubo='PALRS00000005' and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0    then   
          -- --FORMULA  
    case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    ((dop1.Ancho - 0.030)-0.001)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    ((dop1.Ancho - 0.030)-0.001)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    ((dop1.Ancho - 0.020)-0.001)  
    end   
   when dop1.CodigoTubo='PALRS00000024'   and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then   
          -- --FORMULA  
     case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    ((dop1.Ancho - 0.030)-0.001)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    ((dop1.Ancho - 0.030)-0.001)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where       dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    ((dop1.Ancho - 0.020)-0.001)  
    end   
          -- END ((dop1.Ancho - 0.029) -0.001)   
          when  dop1.CodigoTubo='PALLU00000003' and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0    then   
           case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    ((dop1.Ancho - 0.030)-0.001)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    ((dop1.Ancho - 0.030)-0.001)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    ((dop1.Ancho - 0.020)-0.001)  
    end   
    when  dop1.CodigoTubo='PALLU00000003' and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTLU00000002,MOTLU00000004,MOTLU00000003,MOTLU00000001')>0    then   
           case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    ((dop1.Ancho - 0.040)-0.001)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    ((dop1.Ancho - 0.040)-0.001)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    ((dop1.Ancho - 0.040)-0.001)  
    end    
                  
          end  
 --  
      
     when a.CantidadProducto>=4 then   
       --  
          case when dop1.CodigoTubo='PALRS00000003'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.021) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.021) -0.001)  
            when dop1.CodigoTubo='PALRS00000005'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.021) -0.001)  
            when dop1.CodigoTubo='PALRZ00000026'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.021) -0.001)    
            when dop1.CodigoTubo='PALRS00000005'   and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then -- --FORMULA  
     case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    ((dop1.Ancho - 0.030)-0.001)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    ((dop1.Ancho - 0.030)-0.001)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    ((dop1.Ancho - 0.020)-0.001)  
    end   
   when dop1.CodigoTubo='PALRS00000024'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then -- --FORMULA  
     case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    ((dop1.Ancho - 0.030)-0.001)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    ((dop1.Ancho - 0.030)-0.001)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    ((dop1.Ancho - 0.020)-0.001)  
    end   
          -- END ((dop1.Ancho - 0.029) -0.001)   
            when  dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then   
           case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    ((dop1.Ancho - 0.030)-0.001)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    ((dop1.Ancho - 0.030)-0.001)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    ((dop1.Ancho - 0.020)-0.001)  
    end   
   when  dop1.CodigoTubo='PALLU00000003' and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTLU00000002,MOTLU00000004,MOTLU00000003,MOTLU00000001')>0    then   
           case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    ((dop1.Ancho - 0.040)-0.001)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    ((dop1.Ancho - 0.040)-0.001)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    ((dop1.Ancho - 0.040)-0.001)  
    end    
                  
          end  
            
 end  
 --   
end    
  from  TBL_DetalleOrdenProduccion dop1   
LEFT OUTER join Tbl_DetalleOpGrupo lop1 on dop1.CotizacionGrupo = lop1.CotizacionGrupo  
left outer join Tbl_Ambiente a on dop1.IndiceAgrupacion=a.indice and a.NumeroCotizacion=lop1.NumeroCotizacion  
where dop1.Id=dop.Id -- limit 1  
)  
  
  
  
-- ROLLER BASICO TELA  
WHEN   subString(dop.CodigoProducto,1,5)   ='PRTRB' THEN  
(  
select top 1  
case when dop1.Accionamiento='Manual' then  
  
   case when a.CantidadProducto=1 then   
   --  
          case  when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.025) - 0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.025) - 0.001)   
            when dop1.CodigoTubo='PALRS00000005'    then ((dop1.Ancho - 0.030) - 0.001)  
            when dop1.CodigoTubo='PALLU00000003'    then ((dop1.Ancho - 0.030) - 0.001)  
          end  
 --  
      when isnull(a.CantidadProducto,'0')='0' then  
     --  
          case  when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.025) - 0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.025) - 0.001)   
            when dop1.CodigoTubo='PALRS00000005'    then ((dop1.Ancho - 0.030) - 0.001)  
            when dop1.CodigoTubo='PALLU00000003'    then ((dop1.Ancho - 0.030) - 0.001)  
          end  
          --  
     when a.CantidadProducto=2 then  
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.021) - 0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.021) - 0.001)    
            when dop1.CodigoTubo='PALRS00000005'    then ((dop1.Ancho - 0.025) - 0.001)  
            when dop1.CodigoTubo='PALLU00000003'    then ((dop1.Ancho - 0.025) - 0.001)  
          end  
 --  
     when a.CantidadProducto=3 then  
     --  
          case  when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.020) - 0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.020) - 0.001)    
            when dop1.CodigoTubo='PALRS00000005'    then ((dop1.Ancho - 0.021) - 0.001)   
          end  
 --   
    when a.CantidadProducto >= 4 then  
      
          case  when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.019) - 0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.019) - 0.001)    
            when dop1.CodigoTubo='PALRS00000005'    then ((dop1.Ancho - 0.021) - 0.001)   
          end  
   end      
    
end   
  from  TBL_DetalleOrdenProduccion dop1   
LEFT OUTER join Tbl_DetalleOpGrupo lop1 on dop1.CotizacionGrupo = lop1.CotizacionGrupo  
left outer join Tbl_Ambiente a on dop1.IndiceAgrupacion=a.indice and a.NumeroCotizacion=lop1.NumeroCotizacion  
where dop1.Id=dop.Id -- limit 1  
)  
  
  
-- END  
--  Roller Zebra  TELA  
WHEN   subString(dop.CodigoProducto,1,5)   ='PRTRZ' THEN  
(select top 1  
case when dop1.Accionamiento='Manual' then  
 -- N  
   case when a.CantidadProducto=1 then   
   --  
          case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.030) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.030) -0.001)   
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Ancho - 0.030) -0.001)   
    end  
 --  
     when isnull(a.CantidadProducto,'0')='0' then  
     --  
           case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.030) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.030) -0.001)   
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Ancho - 0.030) -0.001)   
          end  
 --  
     when a.CantidadProducto=2 then  
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.021) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.021) -0.001)   
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Ancho - 0.023) -0.001)   
          end  
 --  
     when a.CantidadProducto=3 then  
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.020) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.020) -0.001)   
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Ancho - 0.021) -0.001)   
          end  
 --  
   end   
     
  when dop1.Accionamiento='Motorizado' then   
 case when a.CantidadProducto=1 then   
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.025) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.025) -0.001)   
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Ancho - 0.035) -0.001)   
          end  
 --  
      when isnull(a.CantidadProducto,'0')='0' then   
       --  
           case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.025) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.025) -0.001)   
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Ancho - 0.035) -0.001)   
          end  
          --   
   when a.CantidadProducto=2 then   
       --  
          case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.021) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.021) -0.001)   
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Ancho - 0.023) -0.001)   
          end  
 --  
 end   
end    
  from  TBL_DetalleOrdenProduccion dop1   
LEFT OUTER join Tbl_DetalleOpGrupo lop1 on dop1.CotizacionGrupo = lop1.CotizacionGrupo  
left outer join Tbl_Ambiente a on dop1.IndiceAgrupacion=a.indice and a.NumeroCotizacion=lop1.NumeroCotizacion  
where dop1.Id=dop.Id -- limit 1  
)  
  
  
-- Roller Shangrilla TELA  
WHEN   subString(dop.CodigoProducto,1,5)   ='PRTSH' THEN  
(select top 1  
case when dop1.Accionamiento='Manual' then  
  
   case when a.CantidadProducto=1 then   
   --  
          case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.030) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.030) -0.001)   
          end  
 --  
     when isnull(a.CantidadProducto,'0')='0' then  
     --  
         case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.030) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.030) -0.001)   
          end  
 --  
     when a.CantidadProducto=2 then  
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.021) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.021) -0.001)   
          end  
 --  
     when a.CantidadProducto=3 then  
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.020) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.020) -0.001)   
          end  
 --  
   end   
     
  when dop1.Accionamiento='Motorizado' then   
 case when a.CantidadProducto=1 then   
     --  
          case  when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.025) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.025) -0.001)   
          end  
 --   
       when isnull(a.CantidadProducto,'0')='0' then   
       --  
          case  when dop1.CodigoTubo='PALRS00000003'   then ((dop1.Ancho - 0.021) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.021) -0.001)   
          end  
 --  
   when a.CantidadProducto=2 then   
       --  
          case  when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.021) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.021) -0.001)   
          end  
 --  
 end   
end     
  from  TBL_DetalleOrdenProduccion dop1   
LEFT OUTER join Tbl_DetalleOpGrupo lop1 on dop1.CotizacionGrupo = lop1.CotizacionGrupo  
left outer join Tbl_Ambiente a on dop1.IndiceAgrupacion=a.indice and a.NumeroCotizacion=lop1.NumeroCotizacion  
where dop1.Id=dop.Id  
-- limit 1  
)  
  
ELSE '0'  
end --as TelaCalculo
  

from  TBL_DetalleOrdenProduccion Dop  
Join  Tbl_DetalleOpGrupo Grd ON Grd.CotizacionGrupo= Dop.CotizacionGrupo And Grd.NumeroCotizacion=Dop.NumeroCotizacion
 where substring(dop.CodigoProducto,1,3)='PRT'
  AND  Dop.Id=@ParametroID 
    RETURN @Tela;
END;

CREATE   FUNCTION dbo.CalcularTubo(@ParametroID INT)
RETURNS DECIMAL(18, 3)
AS
BEGIN
    DECLARE @Tubo DECIMAL(18, 3);
SELECT @Tubo =

case when  
 subString(dop.CodigoProducto,1,5) = 'PRTRS' then  
-- ROLLER SHADE   
(select  top 1  
case when dop1.Accionamiento='Manual' then  
   case when a.CantidadProducto=1 then   
   --     
          case   
   when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.025)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.025)  
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Ancho - 0.030)  
            when dop1.CodigoTubo='PALRZ00000026'    then (dop1.Ancho - 0.028)  
            when dop1.CodigoTubo='PALLU00000003'    then (dop1.Ancho - 0.030)    
          end  
   --  
     when isnull(a.CantidadProducto,'0')='0' then  
   --  
           case   
   when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.025)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.025)  
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Ancho - 0.030)  
            when dop1.CodigoTubo='PALRZ00000026'    then (dop1.Ancho - 0.028)  
            when dop1.CodigoTubo='PALLU00000003'    then (dop1.Ancho - 0.030)    
          end  
    --   
     when a.CantidadProducto=2 then  
    --  
          case when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.023)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.023)  
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Ancho - 0.025)  
            when dop1.CodigoTubo='PALRZ00000026'    then (dop1.Ancho - 0.023)    
            when dop1.CodigoTubo='PALLU00000003'    then (dop1.Ancho - 0.025)   
          end  
  --  
     when a.CantidadProducto=3 then  
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.021)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.021)  
            when dop1.CodigoTubo='PALRS00000005'    then  (dop1.Ancho - 0.021)           
            when dop1.CodigoTubo='PALRZ00000026'    then  (dop1.Ancho - 0.021)  
          end  
  --  
     when a.CantidadProducto >=4 then   
      --  
          case when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.021)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.021)  
            when dop1.CodigoTubo='PALRS00000005'    then  (dop1.Ancho - 0.021)   
            when dop1.CodigoTubo='PALRZ00000026'    then (dop1.Ancho - 0.021)   
            -- when dop1.CodigoTubo='PALLU00000003'    then (dop1.Ancho - 0.038)                 
  
          end  
   
 --  
end   
     
  when dop1.Accionamiento='Motorizado' then   
 case when a.CantidadProducto=1 then   
     --    -- BATERIA  
         case when dop1.CodigoTubo='PALRS00000003'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.025)   
            when dop1.CodigoTubo='PALRS00000004'   and dop1.CodigoMotor='MOTRZ00000002'  then (dop1.Ancho - 0.025)  
            when dop1.CodigoTubo='PALRS00000005'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.030)      
            when dop1.CodigoTubo='PALRZ00000026'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.028)   
            when dop1.CodigoTubo='PALLU00000003'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.030)    
             
            -- RAEX              
            when dop1.CodigoTubo='PALRS00000005'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then (dop1.Ancho - 0.038)               
            when dop1.CodigoTubo='PALRS00000024'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then (dop1.Ancho - 0.038)   
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then (dop1.Ancho - 0.040)   
            -- LUTRON              
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTLU00000002,MOTLU00000004,MOTLU00000003,MOTLU00000001')>0   then (dop1.Ancho - 0.040)    
          end  
 when isnull(a.CantidadProducto,'0')='0' then   
       --  
        case when dop1.CodigoTubo='PALRS00000003'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.025)   
            when dop1.CodigoTubo='PALRS00000004'   and dop1.CodigoMotor='MOTRZ00000002'  then (dop1.Ancho - 0.025)  
            when dop1.CodigoTubo='PALRS00000005'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.030)      
            when dop1.CodigoTubo='PALRZ00000026'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.028)   
            when dop1.CodigoTubo='PALLU00000003'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.030)    
            -- RAEX              
            when dop1.CodigoTubo='PALRS00000005'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then (dop1.Ancho - 0.038)               
            when dop1.CodigoTubo='PALRS00000024'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then (dop1.Ancho - 0.038)   
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then (dop1.Ancho - 0.040)   
            -- LUTRON              
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTLU00000002,MOTLU00000004,MOTLU00000003,MOTLU00000001')>0   then (dop1.Ancho - 0.040)    
              
          end  
 --  
   when a.CantidadProducto=2 then   
       --  
          case when dop1.CodigoTubo='PALRS00000003'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.023)   
            when dop1.CodigoTubo='PALRS00000004'   and dop1.CodigoMotor='MOTRZ00000002'  then (dop1.Ancho - 0.023)  
            when dop1.CodigoTubo='PALRS00000005'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.025)      
            when dop1.CodigoTubo='PALRZ00000026'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.023)   
            when dop1.CodigoTubo='PALLU00000003'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.025)    
              
            -- RAEX              
            when dop1.CodigoTubo='PALRS00000005'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then (dop1.Ancho - 0.029)               
            when dop1.CodigoTubo='PALRS00000024'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then (dop1.Ancho - 0.029)   
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then (dop1.Ancho - 0.040)   
            -- LUTRON              
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTLU00000002,MOTLU00000004,MOTLU00000003,MOTLU00000001')>0   then (dop1.Ancho - 0.040)    
          end     
         
 --  
      when a.CantidadProducto=3 then   
       --  
          case     
            when dop1.CodigoTubo='PALRS00000003'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.021)   
            when dop1.CodigoTubo='PALRS00000004'   and dop1.CodigoMotor='MOTRZ00000002'  then (dop1.Ancho - 0.021)  
            when dop1.CodigoTubo='PALRS00000005'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.021)      
            when dop1.CodigoTubo='PALRZ00000026'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.021)    
            -- RAEX                    
      when dop1.CodigoTubo='PALRS00000005'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then   
          -- --FORMULA  
    case when dop1.Id =(select min(dopn1.Id) from TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    (dop1.Ancho - 0.030)  
    --- PRIMERO PRODUCTO   
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    (dop1.Ancho - 0.030)  
    -- ULTIMO PRODUCTO  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    (dop1.Ancho - 0.020)  
    end     
          -- END (dop1.Ancho - 0.029)   
          when dop1.CodigoTubo='PALLU00000003' and  dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then  
           case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    (dop1.Ancho - 0.030)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    (dop1.Ancho - 0.030)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    (dop1.Ancho - 0.020)  
    end   
                  
           when dop1.CodigoTubo='PALRS00000024' and   dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then  
           case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    (dop1.Ancho - 0.030)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    (dop1.Ancho - 0.030)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    (dop1.Ancho - 0.020)  
    end  
   when dop1.CodigoTubo='PALLU00000003' and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTLU00000002,MOTLU00000004,MOTLU00000003,MOTLU00000001')>0   then  
           case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    (dop1.Ancho - 0.040)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    (dop1.Ancho - 0.040)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    (dop1.Ancho - 0.040)  
    end   
       end   
 --  
      when a.CantidadProducto>=4 then   
       --  
          case when dop1.CodigoTubo='PALRS00000003'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.021)   
            when dop1.CodigoTubo='PALRS00000004'   and dop1.CodigoMotor='MOTRZ00000002'  then (dop1.Ancho - 0.021)  
            when dop1.CodigoTubo='PALRS00000005'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.021)      
            when dop1.CodigoTubo='PALRZ00000026'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.021)    
              
            when dop1.CodigoTubo='PALRS00000005'   and   dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then  
          -- --FORMULA  
    case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    (dop1.Ancho - 0.030)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    (dop1.Ancho - 0.030)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    (dop1.Ancho - 0.020)  
    end     
          -- END (dop1.Ancho - 0.029)   
           when dop1.CodigoTubo='PALLU00000003'  and   dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0 then  
            case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    (dop1.Ancho - 0.030)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    (dop1.Ancho - 0.030)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    (dop1.Ancho - 0.020)  
    end     
         
          when dop1.CodigoTubo='PALRS00000024' then  
            case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    (dop1.Ancho - 0.030)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    (dop1.Ancho - 0.030)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    (dop1.Ancho - 0.020)  
    end     
    
   when dop1.CodigoTubo='PALLU00000003' and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTLU00000002,MOTLU00000004,MOTLU00000003,MOTLU00000001')>0   then  
           case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    (dop1.Ancho - 0.040)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    (dop1.Ancho - 0.040)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    (dop1.Ancho - 0.040)  
    end   
       end    
         
 --  
 end   
end   
from  TBL_DetalleOrdenProduccion dop1   
LEFT OUTER join Tbl_DetalleOpGrupo lop1 on dop1.CotizacionGrupo = lop1.CotizacionGrupo  
left outer join Tbl_Ambiente a on dop1.IndiceAgrupacion=a.indice and a.NumeroCotizacion=lop1.NumeroCotizacion  
where dop1.Id=dop.Id   
order by dop1.Id asc   
-- limit 1  
)  
--  Roller Zebra  TUBO  
  
WHEN   subString(dop.CodigoProducto,1,5)   ='PRTRZ' THEN   
(  
select  top 1  
case when dop1.Accionamiento='Manual'  then  
 -- N2  
   case when a.CantidadProducto=1 then   
   --  
          case  when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.030)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.030)   
            when dop1.CodigoTubo='PALRZ00000026'    then (dop1.Ancho - 0.030)   
          end  
 --  
      when isnull(a.CantidadProducto,'0')='0' then  
     --  
          case  when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.030)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.030)   
            when dop1.CodigoTubo='PALRZ00000026'    then (dop1.Ancho - 0.030)   
          end  
            
     when a.CantidadProducto=2 then  
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.021)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.021)   
            when dop1.CodigoTubo='PALRZ00000026'    then (dop1.Ancho - 0.023)   
          end  
 --  
     when a.CantidadProducto=3 then  
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.020)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.020)   
            when dop1.CodigoTubo='PALRZ00000026'    then (dop1.Ancho - 0.021)   
          end  
 --   
   end   
     
  when dop1.Accionamiento='Motorizado' then   
 case when a.CantidadProducto=1 then   
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.025)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.025)   
            when dop1.CodigoTubo='PALRZ00000026'    then (dop1.Ancho - 0.035)   
          end  
 --  
      when isnull(a.CantidadProducto,'0')='0' then   
       --    
       case when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.025)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.025)   
            when dop1.CodigoTubo='PALRZ00000026'    then (dop1.Ancho - 0.035)   
          end  
          --  
   when a.CantidadProducto=2 then   
       --  
          case when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.021)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.021)   
            when dop1.CodigoTubo='PALRZ00000026'    then (dop1.Ancho - 0.023)   
          end  
 --  
 end   
end   
  from  TBL_DetalleOrdenProduccion dop1   
LEFT OUTER join Tbl_DetalleOpGrupo lop1 on dop1.CotizacionGrupo = lop1.CotizacionGrupo  
left outer join Tbl_Ambiente a on dop1.IndiceAgrupacion=a.indice and a.NumeroCotizacion=lop1.NumeroCotizacion  
where dop1.Id=dop.Id -- limit 1  
)  
  
  
  
-- ROLLER BASICO TUBO  
  
WHEN   subString(dop.CodigoProducto,1,5)   ='PRTRB' THEN  
(  
select top 1  
case when dop1.Accionamiento='Manual' then  
  
   case when a.CantidadProducto=1 then   
   --  
          case  when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.025)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.025)    
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Ancho - 0.030)    
            when dop1.CodigoTubo='PALLU00000003'    then (dop1.Ancho - 0.030)    
          end  
 --  
      when isnull(a.CantidadProducto,'0')='0' then  
     --  
         case  when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.025)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.025)    
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Ancho - 0.030)    
            when dop1.CodigoTubo='PALLU00000003'    then (dop1.Ancho - 0.030)    
          end  
 --  
     when a.CantidadProducto=2 then  
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.021)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.021)    
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Ancho - 0.025)    
            when dop1.CodigoTubo='PALLU00000003'    then (dop1.Ancho - 0.025)      
          end  
 --  
     when a.CantidadProducto=3 then  
     --  
          case  when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.020)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.020)    
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Ancho - 0.021)    
          end  
 --   
    when a.CantidadProducto >= 4 then  
      
          case  when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.019)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.019)     
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Ancho - 0.021)    
          end  
   end      
    
end   
  from  TBL_DetalleOrdenProduccion dop1   
LEFT OUTER join Tbl_DetalleOpGrupo lop1 on dop1.CotizacionGrupo = lop1.CotizacionGrupo  
left outer join Tbl_Ambiente a on dop1.IndiceAgrupacion=a.indice and a.NumeroCotizacion=lop1.NumeroCotizacion  
where dop1.Id=dop.Id --limit 1  
)  
  
  
  
-- END  
-- Roller Shangrilla TUBO  
WHEN   subString(dop.CodigoProducto,1,5)   ='PRTSH' THEN  
(select  top 1   
case when dop1.Accionamiento='Manual' then  
  
   case when a.CantidadProducto=1 then   
   --  
          case  when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.030)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.030)   
          end  
 --  
        when isnull(a.CantidadProducto,'0')='0' then  
     --  
             case  when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.030)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.030)   
              end  
 --  
     when a.CantidadProducto=2 then  
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.021)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.021)   
          end  
 --  
     when a.CantidadProducto=3 then  
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.020)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.020)   
          end  
 --   
   end   
     
  when dop1.Accionamiento='Motorizado' then   
 case when a.CantidadProducto=1 then   
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.025)   
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.025)    
          end  
 --  
         when isnull(a.CantidadProducto,'0')='0' then   
       --  
           case when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.025)   
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.025)    
            end  
   when a.CantidadProducto=2 then   
       --  
          case when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.021)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.021)   
          end  
 --  
 end   
end     
  from  TBL_DetalleOrdenProduccion dop1   
LEFT OUTER join Tbl_DetalleOpGrupo lop1 on dop1.CotizacionGrupo = lop1.CotizacionGrupo  
left outer join Tbl_Ambiente a on dop1.IndiceAgrupacion=a.indice and a.NumeroCotizacion=lop1.NumeroCotizacion  
where dop1.Id=dop.Id --limit 1  
)  
ELSE '0'  
end --as TuboCalculo   


from  TBL_DetalleOrdenProduccion Dop  
Join  Tbl_DetalleOpGrupo Grd ON Grd.CotizacionGrupo= Dop.CotizacionGrupo And Grd.NumeroCotizacion=Dop.NumeroCotizacion
 where substring(dop.CodigoProducto,1,3)='PRT'
 AND  Dop.Id=@ParametroID 
    RETURN @Tubo;
END;


 CREATE   FUNCTION dbo.CalcularAltura(@ParametroID INT)
RETURNS DECIMAL(18, 3)
AS
BEGIN
    DECLARE @Altura DECIMAL(18, 3);
SELECT @Altura = 

 
-- --ALTURA----  
case when   
 subString(dop.CodigoProducto,1,5)   ='PRTRS' then  
-- ROLLER SHADE   
(  
select  top 1  
case when dop1.Accionamiento='Manual' then  
  
   case when a.CantidadProducto=1 then   
   --  
          case   
   when dop1.CodigoTubo='PALRS00000003'    then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALRZ00000026'    then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALLU00000003'    then (dop1.Alto + 0.30)    
          end  
 --    
    when isnull(a.CantidadProducto,'0')='0' then  
     --  
         case   
   when dop1.CodigoTubo='PALRS00000003'    then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALRZ00000026'    then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALLU00000003'    then (dop1.Alto + 0.30)    
          end  
 --  
     when a.CantidadProducto=2 then  
     --  
         case   
   when dop1.CodigoTubo='PALRS00000003'    then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALRZ00000026'    then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALLU00000003'    then (dop1.Alto + 0.30)   
          end  
 --  
     when a.CantidadProducto=3 then  
     --  
          case   
   when dop1.CodigoTubo='PALRS00000003'    then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALRZ00000026'    then (dop1.Alto + 0.25)  
               
          end  
 --  
     when a.CantidadProducto >=4 then   
      --  
          case   
   when dop1.CodigoTubo='PALRS00000003'    then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALRZ00000026'    then (dop1.Alto + 0.25)  
              
          end   
            
 --  
   end   
     
  when dop1.Accionamiento='Motorizado' then   
 case when a.CantidadProducto=1 then   
     --  
          case   
            when dop1.CodigoTubo='PALRS00000003'   and dop1.CodigoMotor='MOTRZ00000002' then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000004'  and dop1.CodigoMotor='MOTRZ00000002'  then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000005'   and dop1.CodigoMotor='MOTRZ00000002' then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALRZ00000026'   and dop1.CodigoMotor='MOTRZ00000002' then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALLU00000003'  and dop1.CodigoMotor='MOTRZ00000002'  then (dop1.Alto + 0.30)  
            -- RAEX  
              
            when dop1.CodigoTubo='PALRS00000005'   and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALRS00000024'   and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then (dop1.Alto + 0.30)  
            -- LUTRON              
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTLU00000002,MOTLU00000004,MOTLU00000003,MOTLU00000001')>0  then (dop1.Alto + 0.30)  
          end  
  --  
       when isnull(a.CantidadProducto,'0')='0' then   
       --  
          case   
            when dop1.CodigoTubo='PALRS00000003'   and dop1.CodigoMotor='MOTRZ00000002' then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000004'  and dop1.CodigoMotor='MOTRZ00000002'  then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000005'   and dop1.CodigoMotor='MOTRZ00000002' then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALRZ00000026'   and dop1.CodigoMotor='MOTRZ00000002' then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALLU00000003'  and dop1.CodigoMotor='MOTRZ00000002'  then (dop1.Alto + 0.30)  
              
            when dop1.CodigoTubo='PALRS00000005'   and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALRS00000024'   and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then (dop1.Alto + 0.30)  
            -- LUTRON              
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTLU00000002,MOTLU00000004,MOTLU00000003,MOTLU00000001')>0  then (dop1.Alto + 0.30)  
  end  
 --  
   when a.CantidadProducto=2 then   
       --  
          case   
            when dop1.CodigoTubo='PALRS00000003'   and dop1.CodigoMotor='MOTRZ00000002' then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000004'  and dop1.CodigoMotor='MOTRZ00000002'  then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000005'   and dop1.CodigoMotor='MOTRZ00000002' then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALRZ00000026'   and dop1.CodigoMotor='MOTRZ00000002' then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALLU00000003'  and dop1.CodigoMotor='MOTRZ00000002'  then (dop1.Alto + 0.30)  
            -- RAEX              
            when dop1.CodigoTubo='PALRS00000005'   and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALRS00000024'   and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then (dop1.Alto + 0.30)  
            -- LUTRON              
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTLU00000002,MOTLU00000004,MOTLU00000003,MOTLU00000001')>0  then (dop1.Alto + 0.30)  
          end  
 --  
      when a.CantidadProducto=3 then   
       --  
          case   
            when dop1.CodigoTubo='PALRS00000003'   and dop1.CodigoMotor='MOTRZ00000002' then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000004'  and dop1.CodigoMotor='MOTRZ00000002'  then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000005'   and dop1.CodigoMotor='MOTRZ00000002' then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALRZ00000026'   and dop1.CodigoMotor='MOTRZ00000002' then (dop1.Alto + 0.25)  
              
            when dop1.CodigoTubo='PALRS00000005'   and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0 then   
             -- --FORMULA  
    case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    (dop1.Alto + 0.25)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    (dop1.Alto + 0.25)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    (dop1.Alto + 0.25)  
    end   
    when dop1.CodigoTubo='PALRS00000024' and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then   
             -- --FORMULA  
    case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    (dop1.Alto + 0.25)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    (dop1.Alto + 0.25)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    (dop1.Alto + 0.25)  
    end   
          -- END (dop1.Alto + 0.25)   
            when  dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then   
           case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    (dop1.Alto + 0.30)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    (dop1.Alto + 0.30)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    (dop1.Alto + 0.30)  
    end   
    when  dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTLU00000002,MOTLU00000004,MOTLU00000003,MOTLU00000001')>0   then   
           case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    (dop1.Alto + 0.30)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    (dop1.Alto + 0.30)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    (dop1.Alto + 0.30)  
    end   
          end  
 --  
      when a.CantidadProducto>=4 then   
       --  
          case   
            
            when dop1.CodigoTubo='PALRS00000003'   and dop1.CodigoMotor='MOTRZ00000002' then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000004'  and dop1.CodigoMotor='MOTRZ00000002'  then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000005'   and dop1.CodigoMotor='MOTRZ00000002' then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALRZ00000026'   and dop1.CodigoMotor='MOTRZ00000002' then (dop1.Alto + 0.25)   
              
            when dop1.CodigoTubo='PALRS00000005' and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then   
             -- --FORMULA  
    case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    (dop1.Alto + 0.25)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    (dop1.Alto + 0.25)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    (dop1.Alto + 0.25)  
    end   
     when dop1.CodigoTubo='PALRS00000024' and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then   
             -- --FORMULA  
    case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    (dop1.Alto + 0.25)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    (dop1.Alto + 0.25)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    (dop1.Alto + 0.25)  
    end   
          -- END (dop1.Alto + 0.25)   
           when  dop1.CodigoTubo='PALLU00000003' and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then   
           case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    (dop1.Alto + 0.30)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    (dop1.Alto + 0.30)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    (dop1.Alto + 0.30)  
    end   
   when  dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTLU00000002,MOTLU00000004,MOTLU00000003,MOTLU00000001')>0   then   
           case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    (dop1.Alto + 0.30)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    (dop1.Alto + 0.30)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    (dop1.Alto + 0.30)  
    end   
          end  
 --  
 --  
 end   
end     
  from  TBL_DetalleOrdenProduccion dop1   
LEFT OUTER join Tbl_DetalleOpGrupo lop1 on dop1.CotizacionGrupo = lop1.CotizacionGrupo  
left outer join Tbl_Ambiente a on dop1.IndiceAgrupacion=a.indice and a.NumeroCotizacion=lop1.NumeroCotizacion  
where dop1.Id=dop.Id --limit 1  
)  
  
  
  
-- ROLLER BASICO ALTURA  
  
WHEN   subString(dop.CodigoProducto,1,5)   ='PRTRB' THEN  
(  
select top 1  
case when dop1.Accionamiento='Manual' then  
  
   case when a.CantidadProducto=1 then   
   --  
          case  when dop1.CodigoTubo='PALRS00000003'    then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Alto + 0.20)    
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Alto + 0.25)    
            when dop1.CodigoTubo='PALLU00000003'    then (dop1.Alto + 0.30)    
          end  
 --  
         when isnull(a.CantidadProducto,'0')='0' then  
     --  
       case  when dop1.CodigoTubo='PALRS00000003'    then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Alto + 0.20)    
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Alto + 0.25)    
            when dop1.CodigoTubo='PALLU00000003'    then (dop1.Alto + 0.30)    
          end  
 --  
     when a.CantidadProducto=2 then  
     --  
          case  when dop1.CodigoTubo='PALRS00000003'    then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000004'   then (dop1.Alto + 0.20)      
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Alto + 0.25)    
            when dop1.CodigoTubo='PALLU00000003'    then (dop1.Alto + 0.30)    
          end  
 --  
     when a.CantidadProducto=3 then  
     --  
          case   when dop1.CodigoTubo='PALRS00000003'    then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Alto + 0.20)     
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Alto + 0.25)   
          end  
 --   
    when a.CantidadProducto >= 4 then  
      
          case   when dop1.CodigoTubo='PALRS00000003'    then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Alto + 0.20)     
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Alto + 0.25)   
          end  
   end      
    
end   
  from  TBL_DetalleOrdenProduccion dop1   
LEFT OUTER join Tbl_DetalleOpGrupo lop1 on dop1.CotizacionGrupo = lop1.CotizacionGrupo  
left outer join Tbl_Ambiente a on dop1.IndiceAgrupacion=a.indice and a.NumeroCotizacion=lop1.NumeroCotizacion  
where dop1.Id=dop.Id  
)  
  
  
-- END  
--  Roller Zebra ALTURA  
WHEN   subString(dop.CodigoProducto,1,5)   ='PRTRZ' THEN  
(select top 1  
case when dop1.Accionamiento='Manual' then  
 -- N1  
   case when a.CantidadProducto=1 then   
   --  
          case  when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Alto*2) + 0.40)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Alto*2) + 0.40)   
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Alto*2) + 0.45)    
          end  
 --  
     when isnull(a.CantidadProducto,'0')='0' then  
     --  
         case  when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Alto*2) + 0.40)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Alto*2) + 0.40)   
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Alto*2) + 0.45)    
          end  
 --  
     when a.CantidadProducto=2 then  
     --  
          case   when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Alto*2) + 0.40)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Alto*2) + 0.40)   
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Alto*2) + 0.45)    
          end  
 --  
     when a.CantidadProducto=3 then  
     --  
          case   when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Alto*2) + 0.40)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Alto*2) + 0.40)   
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Alto*2) + 0.45)    
          end  
 --   
   end   
     
  when dop1.Accionamiento='Motorizado' then   
 case when a.CantidadProducto=1 then   
     --  
          case   when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Alto*2) + 0.40)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Alto*2) + 0.40)   
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Alto*2) + 0.45)    
          end  
 --  
      when isnull(a.CantidadProducto,'0')='0' then   
       --  
     case   when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Alto*2) + 0.40)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Alto*2) + 0.40)   
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Alto*2) + 0.45)    
          end  
   when a.CantidadProducto=2 then   
       --  
          case   when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Alto*2) + 0.40)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Alto*2) + 0.40)   
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Alto*2) + 0.45)    
          end  
 --  
 end   
end    
  from  TBL_DetalleOrdenProduccion dop1   
LEFT OUTER join Tbl_DetalleOpGrupo lop1 on dop1.CotizacionGrupo = lop1.CotizacionGrupo  
left outer join Tbl_Ambiente a on dop1.IndiceAgrupacion=a.indice and a.NumeroCotizacion=lop1.NumeroCotizacion  
where dop1.Id=dop.Id  
)  
  
-- Roller Shangrilla ALTURA  
WHEN   subString(dop.CodigoProducto,1,5)   ='PRTSH' THEN  
(  
select  top 1  
case when dop1.Accionamiento='Manual' then    
   case when a.CantidadProducto=1 then   
   --  
          case    
   when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Alto*2) + 0.40)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Alto*2) + 0.40)  
          end  
 --  
     when isnull(a.CantidadProducto,'0')='0' then  
     --  
         case    
   when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Alto*2) + 0.40)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Alto*2) + 0.40)  
          end  
 --  
     when a.CantidadProducto=2 then  
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Alto*2) + 0.40)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Alto*2) + 0.40)  
          end  
 --  
     when a.CantidadProducto=3 then  
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Alto*2) + 0.40)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Alto*2) + 0.40)  
          end  
 --   
   end   
     
  when dop1.Accionamiento='Motorizado' then   
 case when a.CantidadProducto=1 then   
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Alto*2) + 0.40)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Alto*2) + 0.40)  
          end  
 --  
      when isnull(a.CantidadProducto,'0')='0' then   
       --  
             case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Alto*2) + 0.40)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Alto*2) + 0.40)  
          end  
   when a.CantidadProducto=2 then   
       --  
          case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Alto*2) + 0.40)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Alto*2) + 0.40)  
          end  
 --  
 end   
end    
  from  TBL_DetalleOrdenProduccion dop1   
LEFT OUTER join Tbl_DetalleOpGrupo lop1 on dop1.CotizacionGrupo = lop1.CotizacionGrupo  
left outer join Tbl_Ambiente a on dop1.IndiceAgrupacion=a.indice and a.NumeroCotizacion=lop1.NumeroCotizacion  
where dop1.Id=dop.Id)  
ELSE '0'  
end -- as AlturaCalculo
 
from  TBL_DetalleOrdenProduccion Dop  
Join  Tbl_DetalleOpGrupo Grd ON Grd.CotizacionGrupo= Dop.CotizacionGrupo And Grd.NumeroCotizacion=Dop.NumeroCotizacion
 where substring(dop.CodigoProducto,1,3)='PRT'
 AND  Dop.Id=@ParametroID 
    RETURN @Altura;
END;


Create Proc SP_ListarProdParaEstacionXGrupo  
@GrupoCotizacion varchar(50)
as
BEGIN

DECLARE @ESTACION VARCHAR(50)
SET @ESTACION='Ensable1'

IF @ESTACION='Ensable1' 
BEGIN
 -- INIT
 	SELECT 
         STRING_AGG(CAST(Dop.Id AS VARCHAR(50)), '-') AS Id,
            dop.CodigoSisgeco,
            GRD.NumeroCotizacion,
            Grd.CotizacionGrupo,
            dop.CodigoProducto,
            Max(dop.NombreProducto) as NombreProducto,
            Max(dop.Lamina) AS Lamina,
             dop.CodigoCadena,
			Max(Dop.Baston) as Baston,
            Max(dop.CodigoBastonVarrilla),
            dop.CodigoCordon,
            dop.CodigoCordonTipo2,
            Max(dop.CodigoTela)   AS Cod_Tela,
            Max(dop.Tela)   AS Nombre_Tela,
            Max(dop.CodigoRiel) AS CodigoRiel,
            Max(dop.CodigoBaston) AS CodigoBaston,
            Max(dop.CodigoTubo)    AS Cod_Tubo,
            Max(NombreTubo) AS Nombre_Tubo,
            Max(dop.CodigoSwitch) AS CodigoSwitch,
            Max(SUBSTRING(dop.CodigoSisgeco, 1, 5)) AS FamiliaProducto,
            1 AS est, 
            dop.Accionamiento,
            Max(dop.TipoSuperior) AS TipoSuperior,
            SUM(dbo.CalcularTubo(Dop.Id)) AS TuboCalculo,   
            SUM(dbo.CalcularTela(Dop.Id)) AS TelaCalculo,   
            SUM(dbo.CalcularAltura(Dop.Id)) AS AlturaCalculo,
            CAST(SUM((dbo.CalcularTela(Dop.Id) * dbo.CalcularAltura(Dop.Id))) AS DECIMAL(18,3)) AS Area,
            NULL AS LaminaUnidad,
            NULL AS CadenaUnidad,
            NULL AS BastonVarrillaUnidad,
            NULL AS CordonUnidad,
            NULL AS CordonTipo2Unidad,
            NULL AS rielUnidad,
            NULL AS bastonUnidad,
            NULL AS Cod_TuboUnidad,
            NULL AS Cod_TelaUnidad
        FROM  
            TBL_DetalleOrdenProduccion Dop  
        JOIN  
            Tbl_DetalleOpGrupo Grd ON Grd.CotizacionGrupo = Dop.CotizacionGrupo 
                                    AND Grd.NumeroCotizacion = Dop.NumeroCotizacion
        WHERE 
            SUBSTRING(dop.CodigoProducto, 1, 3) = 'PRT' 
			AND GRD.CotizacionGrupo=@GrupoCotizacion
			-- AND SUBSTRING(dop.CodigoProducto, 1, 5) in('PRTCS')
        GROUP BY 
             dop.CodigoSisgeco,GRD.NumeroCotizacion,Grd.CotizacionGrupo,
            dop.CodigoProducto ,   dop.CodigoCadena,dop.CodigoCordon,dop.CodigoCordonTipo2,   dop.Accionamiento
			 

 -- END
 END
 
ELSE IF  @ESTACION='Corte1' 
BEGIN
-- ROLLER SHADE estacion 1
			SELECT 
         STRING_AGG(CAST(Dop.Id AS VARCHAR(50)), '-') AS Id,
            dop.CodigoSisgeco,
            GRD.NumeroCotizacion,
            Grd.CotizacionGrupo,
            dop.CodigoProducto,
            Max(dop.NombreProducto) as NombreProducto,
            Max(dop.Lamina) AS Lamina,
            Max(dop.CodigoCadena) AS CodigoCadena,
			Dop.Baston,
            dop.CodigoBastonVarrilla,
            Max(dop.CodigoCordon) AS CodigoCordon,
            Max(dop.CodigoCordonTipo2) AS CodigoCordonTipo2,
            Max(dop.CodigoTela)   AS Cod_Tela,
            Max(dop.Tela)   AS Nombre_Tela,
            dop.CodigoRiel AS CodigoRiel,
            Max(dop.CodigoBaston) AS CodigoBaston,
             dop.CodigoTubo    AS Cod_Tubo,
            Max(NombreTubo) AS Nombre_Tubo,
            Max(dop.CodigoSwitch) AS CodigoSwitch,
            Max(SUBSTRING(dop.CodigoSisgeco, 1, 5)) AS FamiliaProducto,
            1 AS est, 
            dop.Accionamiento,
            Max(dop.TipoSuperior) AS TipoSuperior,
            SUM(dbo.CalcularTubo(Dop.Id)) AS TuboCalculo,   
            SUM(dbo.CalcularTela(Dop.Id)) AS TelaCalculo,   
            SUM(dbo.CalcularAltura(Dop.Id)) AS AlturaCalculo,
            CAST(SUM((dbo.CalcularTela(Dop.Id) * dbo.CalcularAltura(Dop.Id))) AS DECIMAL(18,3)) AS Area,
            NULL AS LaminaUnidad,
            NULL AS CadenaUnidad,
            NULL AS BastonVarrillaUnidad,
            NULL AS CordonUnidad,
            NULL AS CordonTipo2Unidad,
            NULL AS rielUnidad,
            NULL AS bastonUnidad,
            NULL AS Cod_TuboUnidad,
            NULL AS Cod_TelaUnidad
        FROM  
            TBL_DetalleOrdenProduccion Dop  
        JOIN  
            Tbl_DetalleOpGrupo Grd ON Grd.CotizacionGrupo = Dop.CotizacionGrupo 
                                    AND Grd.NumeroCotizacion = Dop.NumeroCotizacion
        WHERE 
            SUBSTRING(dop.CodigoProducto, 1, 3) = 'PRT' 
			AND GRD.CotizacionGrupo=@GrupoCotizacion
			-- AND SUBSTRING(dop.CodigoProducto, 1, 5) in('PRTPH','PRTPJ','PRTRF')
        GROUP BY 
             dop.CodigoSisgeco,GRD.NumeroCotizacion,Grd.CotizacionGrupo,
            dop.CodigoProducto ,   dop.CodigoTubo,   Dop.Baston,Dop.CodigoBastonVarrilla,Dop.Lamina,Dop.CodigoRiel ,
            dop.Accionamiento
END
ELSE IF  @ESTACION='Corte2' 
BEGIN
-- // ROLLER SHADE ESTACION 2 : TELA
			SELECT 
         STRING_AGG(CAST(Dop.Id AS VARCHAR(50)), '-') AS Id,
            dop.CodigoSisgeco,
            GRD.NumeroCotizacion,
            Grd.CotizacionGrupo,
            dop.CodigoProducto,
            Max(dop.NombreProducto) as NombreProducto,
            Max(dop.Lamina) AS Lamina,
            Max(dop.CodigoCadena) AS CodigoCadena,
			Max(Dop.Baston) as Baston,
            Max(dop.CodigoBastonVarrilla),
            Max(dop.CodigoCordon) AS CodigoCordon,
            Max(dop.CodigoCordonTipo2) AS CodigoCordonTipo2,
            dop.CodigoTela   AS Cod_Tela,
            Max(dop.Tela)   AS Nombre_Tela,
            Max(dop.CodigoRiel) AS CodigoRiel,
            Max(dop.CodigoBaston) AS CodigoBaston,
            Max(dop.CodigoTubo)    AS Cod_Tubo,
            Max(NombreTubo) AS Nombre_Tubo,
            Max(dop.CodigoSwitch) AS CodigoSwitch,
            Max(SUBSTRING(dop.CodigoSisgeco, 1, 5)) AS FamiliaProducto,
            1 AS est, 
            dop.Accionamiento,
            Max(dop.TipoSuperior) AS TipoSuperior,
            SUM(dbo.CalcularTubo(Dop.Id)) AS TuboCalculo,   
            SUM(dbo.CalcularTela(Dop.Id)) AS TelaCalculo,   
            SUM(dbo.CalcularAltura(Dop.Id)) AS AlturaCalculo,
            CAST(SUM((dbo.CalcularTela(Dop.Id) * dbo.CalcularAltura(Dop.Id))) AS DECIMAL(18,3)) AS Area,
            NULL AS LaminaUnidad,
            NULL AS CadenaUnidad,
            NULL AS BastonVarrillaUnidad,
            NULL AS CordonUnidad,
            NULL AS CordonTipo2Unidad,
            NULL AS rielUnidad,
            NULL AS bastonUnidad,
            NULL AS Cod_TuboUnidad,
            NULL AS Cod_TelaUnidad
        FROM  
            TBL_DetalleOrdenProduccion Dop  
        JOIN  
            Tbl_DetalleOpGrupo Grd ON Grd.CotizacionGrupo = Dop.CotizacionGrupo 
                                    AND Grd.NumeroCotizacion = Dop.NumeroCotizacion
        WHERE 
            SUBSTRING(dop.CodigoProducto, 1, 3) = 'PRT' 
			AND GRD.CotizacionGrupo=@GrupoCotizacion
			-- AND SUBSTRING(dop.CodigoProducto, 1, 5) in('PRTPH','PRTPJ','PRTRF')
        GROUP BY 
             dop.CodigoSisgeco,GRD.NumeroCotizacion,Grd.CotizacionGrupo,
            dop.CodigoProducto ,   dop.CodigoTela,  dop.Accionamiento
			 
END
ELSE
BEGIN

SELECT 
         STRING_AGG(CAST(Dop.Id AS VARCHAR(50)), '-') AS Id,
            dop.CodigoSisgeco,
            GRD.NumeroCotizacion,
            Grd.CotizacionGrupo,
            Max(dop.CodigoProducto) as CodigoProducto,
            Max(dop.NombreProducto) as NombreProducto,
            Max(dop.Lamina) AS Lamina,
            Max(dop.CodigoCadena) as CodigoCadena,
			Max(Dop.Baston) as Baston,
            Max(dop.CodigoBastonVarrilla),
            Max(dop.CodigoCordon) as CodigoCordon,
            Max(dop.CodigoCordonTipo2) as CodigoCordonTipo2,
            Max(dop.CodigoTela)   AS Cod_Tela,
            Max(dop.Tela)   AS Nombre_Tela,
            Max(dop.CodigoRiel) AS CodigoRiel,
            Max(dop.CodigoBaston) AS CodigoBaston,
            Max(dop.CodigoTubo)    AS Cod_Tubo,
            Max(NombreTubo) AS Nombre_Tubo,
            Max(dop.CodigoSwitch) AS CodigoSwitch,
            Max(SUBSTRING(dop.CodigoSisgeco, 1, 5)) AS FamiliaProducto,
            1 AS est, 
            dop.Accionamiento,
            Max(dop.TipoSuperior) AS TipoSuperior,
            SUM(dbo.CalcularTubo(Dop.Id)) AS TuboCalculo,   
            SUM(dbo.CalcularTela(Dop.Id)) AS TelaCalculo,   
            SUM(dbo.CalcularAltura(Dop.Id)) AS AlturaCalculo,
            CAST(SUM((dbo.CalcularTela(Dop.Id) * dbo.CalcularAltura(Dop.Id))) AS DECIMAL(18,3)) AS Area,
            NULL AS LaminaUnidad,
            NULL AS CadenaUnidad,
            NULL AS BastonVarrillaUnidad,
            NULL AS CordonUnidad,
            NULL AS CordonTipo2Unidad,
            NULL AS rielUnidad,
            NULL AS bastonUnidad,
            NULL AS Cod_TuboUnidad,
            NULL AS Cod_TelaUnidad
        FROM  
            TBL_DetalleOrdenProduccion Dop  
        JOIN  
            Tbl_DetalleOpGrupo Grd ON Grd.CotizacionGrupo = Dop.CotizacionGrupo 
                                    AND Grd.NumeroCotizacion = Dop.NumeroCotizacion
        WHERE 
            SUBSTRING(dop.CodigoProducto, 1, 3) = 'PRT' 
			AND GRD.CotizacionGrupo=@GrupoCotizacion
			-- AND SUBSTRING(dop.CodigoProducto, 1, 5) in('PRTPH','PRTPJ','PRTRF')
        GROUP BY 
             dop.CodigoSisgeco,GRD.NumeroCotizacion,Grd.CotizacionGrupo,
			 dop.Accionamiento
 
END

END  

 ALTER PROCEDURE [dbo].[SP_GetLayoutGrupo]      
    @NumeroCotizacionGrupo NVARCHAR(50)      
 AS BEGIN  

select     
case when  
 subString(dop.CodigoProducto,1,5) = 'PRTRS' then  
-- ROLLER SHADE   
(select  top 1  
case when dop1.Accionamiento='Manual' then  
   case when a.CantidadProducto=1 then   
   --     
          case   
   when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.025)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.025)  
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Ancho - 0.030)  
            when dop1.CodigoTubo='PALRZ00000026'    then (dop1.Ancho - 0.028)  
            when dop1.CodigoTubo='PALLU00000003'    then (dop1.Ancho - 0.030)    
          end  
   --  
     when isnull(a.CantidadProducto,'0')='0' then  
   --  
           case   
   when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.025)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.025)  
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Ancho - 0.030)  
            when dop1.CodigoTubo='PALRZ00000026'    then (dop1.Ancho - 0.028)  
            when dop1.CodigoTubo='PALLU00000003'    then (dop1.Ancho - 0.030)    
          end  
    --   
     when a.CantidadProducto=2 then  
    --  
          case when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.023)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.023)  
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Ancho - 0.025)  
            when dop1.CodigoTubo='PALRZ00000026'    then (dop1.Ancho - 0.023)    
            when dop1.CodigoTubo='PALLU00000003'    then (dop1.Ancho - 0.025)   
          end  
  --  
     when a.CantidadProducto=3 then  
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.021)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.021)  
            when dop1.CodigoTubo='PALRS00000005'    then  (dop1.Ancho - 0.021)           
            when dop1.CodigoTubo='PALRZ00000026'    then  (dop1.Ancho - 0.021)  
          end  
  --  
     when a.CantidadProducto >=4 then   
      --  
          case when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.021)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.021)  
            when dop1.CodigoTubo='PALRS00000005'    then  (dop1.Ancho - 0.021)   
            when dop1.CodigoTubo='PALRZ00000026'    then (dop1.Ancho - 0.021)   
            -- when dop1.CodigoTubo='PALLU00000003'    then (dop1.Ancho - 0.038)                 
  
          end  
   
 --  
end   
     
  when dop1.Accionamiento='Motorizado' then   
 case when a.CantidadProducto=1 then   
     --    -- BATERIA  
         case when dop1.CodigoTubo='PALRS00000003'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.025)   
            when dop1.CodigoTubo='PALRS00000004'   and dop1.CodigoMotor='MOTRZ00000002'  then (dop1.Ancho - 0.025)  
            when dop1.CodigoTubo='PALRS00000005'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.030)      
            when dop1.CodigoTubo='PALRZ00000026'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.028)   
            when dop1.CodigoTubo='PALLU00000003'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.030)    
             
            -- RAEX              
            when dop1.CodigoTubo='PALRS00000005'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then (dop1.Ancho - 0.038)               
            when dop1.CodigoTubo='PALRS00000024'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then (dop1.Ancho - 0.038)   
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then (dop1.Ancho - 0.040)   
            -- LUTRON              
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTLU00000002,MOTLU00000004,MOTLU00000003,MOTLU00000001')>0   then (dop1.Ancho - 0.040)    
          end  
 when isnull(a.CantidadProducto,'0')='0' then   
       --  
        case when dop1.CodigoTubo='PALRS00000003'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.025)   
            when dop1.CodigoTubo='PALRS00000004'   and dop1.CodigoMotor='MOTRZ00000002'  then (dop1.Ancho - 0.025)  
            when dop1.CodigoTubo='PALRS00000005'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.030)      
            when dop1.CodigoTubo='PALRZ00000026'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.028)   
            when dop1.CodigoTubo='PALLU00000003'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.030)    
            -- RAEX              
            when dop1.CodigoTubo='PALRS00000005'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then (dop1.Ancho - 0.038)               
            when dop1.CodigoTubo='PALRS00000024'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then (dop1.Ancho - 0.038)   
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then (dop1.Ancho - 0.040)   
            -- LUTRON              
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTLU00000002,MOTLU00000004,MOTLU00000003,MOTLU00000001')>0   then (dop1.Ancho - 0.040)    
              
          end  
 --  
   when a.CantidadProducto=2 then   
       --  
          case when dop1.CodigoTubo='PALRS00000003'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.023)   
            when dop1.CodigoTubo='PALRS00000004'   and dop1.CodigoMotor='MOTRZ00000002'  then (dop1.Ancho - 0.023)  
            when dop1.CodigoTubo='PALRS00000005'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.025)      
            when dop1.CodigoTubo='PALRZ00000026'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.023)   
            when dop1.CodigoTubo='PALLU00000003'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.025)    
              
            -- RAEX              
            when dop1.CodigoTubo='PALRS00000005'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then (dop1.Ancho - 0.029)               
            when dop1.CodigoTubo='PALRS00000024'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then (dop1.Ancho - 0.029)   
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then (dop1.Ancho - 0.040)   
            -- LUTRON              
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTLU00000002,MOTLU00000004,MOTLU00000003,MOTLU00000001')>0   then (dop1.Ancho - 0.040)    
          end     
         
 --  
      when a.CantidadProducto=3 then   
       --  
          case     
            when dop1.CodigoTubo='PALRS00000003'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.021)   
            when dop1.CodigoTubo='PALRS00000004'   and dop1.CodigoMotor='MOTRZ00000002'  then (dop1.Ancho - 0.021)  
            when dop1.CodigoTubo='PALRS00000005'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.021)      
            when dop1.CodigoTubo='PALRZ00000026'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.021)    
            -- RAEX                    
      when dop1.CodigoTubo='PALRS00000005'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then   
          -- --FORMULA  
    case when dop1.Id =(select min(dopn1.Id) from TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    (dop1.Ancho - 0.030)  
    --- PRIMERO PRODUCTO   
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    (dop1.Ancho - 0.030)  
    -- ULTIMO PRODUCTO  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    (dop1.Ancho - 0.020)  
    end     
          -- END (dop1.Ancho - 0.029)   
          when dop1.CodigoTubo='PALLU00000003' and  dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then  
           case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    (dop1.Ancho - 0.030)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    (dop1.Ancho - 0.030)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    (dop1.Ancho - 0.020)  
    end   
                  
           when dop1.CodigoTubo='PALRS00000024' and   dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then  
           case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    (dop1.Ancho - 0.030)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    (dop1.Ancho - 0.030)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    (dop1.Ancho - 0.020)  
    end  
   when dop1.CodigoTubo='PALLU00000003' and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTLU00000002,MOTLU00000004,MOTLU00000003,MOTLU00000001')>0   then  
           case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    (dop1.Ancho - 0.040)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    (dop1.Ancho - 0.040)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    (dop1.Ancho - 0.040)  
    end   
       end   
 --  
      when a.CantidadProducto>=4 then   
       --  
          case when dop1.CodigoTubo='PALRS00000003'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.021)   
            when dop1.CodigoTubo='PALRS00000004'   and dop1.CodigoMotor='MOTRZ00000002'  then (dop1.Ancho - 0.021)  
            when dop1.CodigoTubo='PALRS00000005'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.021)      
            when dop1.CodigoTubo='PALRZ00000026'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.021)    
              
            when dop1.CodigoTubo='PALRS00000005'   and   dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then  
          -- --FORMULA  
    case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    (dop1.Ancho - 0.030)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    (dop1.Ancho - 0.030)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    (dop1.Ancho - 0.020)  
    end     
          -- END (dop1.Ancho - 0.029)   
           when dop1.CodigoTubo='PALLU00000003'  and   dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0 then  
            case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    (dop1.Ancho - 0.030)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    (dop1.Ancho - 0.030)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    (dop1.Ancho - 0.020)  
    end     
         
          when dop1.CodigoTubo='PALRS00000024' then  
            case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    (dop1.Ancho - 0.030)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    (dop1.Ancho - 0.030)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    (dop1.Ancho - 0.020)  
    end     
    
   when dop1.CodigoTubo='PALLU00000003' and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTLU00000002,MOTLU00000004,MOTLU00000003,MOTLU00000001')>0   then  
           case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    (dop1.Ancho - 0.040)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    (dop1.Ancho - 0.040)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    (dop1.Ancho - 0.040)  
    end   
       end    
         
 --  
 end   
end   
from  TBL_DetalleOrdenProduccion dop1   
LEFT OUTER join Tbl_DetalleOpGrupo lop1 on dop1.CotizacionGrupo = lop1.CotizacionGrupo  
left outer join Tbl_Ambiente a on dop1.IndiceAgrupacion=a.indice and a.NumeroCotizacion=lop1.NumeroCotizacion  
where dop1.Id=dop.Id   
order by dop1.Id asc   
-- limit 1  
)  
--  Roller Zebra  TUBO  
  
WHEN   subString(dop.CodigoProducto,1,5)   ='PRTRZ' THEN   
(  
select  top 1  
case when dop1.Accionamiento='Manual'  then  
 -- N2  
   case when a.CantidadProducto=1 then   
   --  
          case  when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.030)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.030)   
            when dop1.CodigoTubo='PALRZ00000026'    then (dop1.Ancho - 0.030)   
          end  
 --  
      when isnull(a.CantidadProducto,'0')='0' then  
     --  
          case  when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.030)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.030)   
            when dop1.CodigoTubo='PALRZ00000026'    then (dop1.Ancho - 0.030)   
          end  
            
     when a.CantidadProducto=2 then  
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.021)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.021)   
            when dop1.CodigoTubo='PALRZ00000026'    then (dop1.Ancho - 0.023)   
          end  
 --  
     when a.CantidadProducto=3 then  
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.020)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.020)   
            when dop1.CodigoTubo='PALRZ00000026'    then (dop1.Ancho - 0.021)   
          end  
 --   
   end   
     
  when dop1.Accionamiento='Motorizado' then   
 case when a.CantidadProducto=1 then   
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.025)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.025)   
            when dop1.CodigoTubo='PALRZ00000026'    then (dop1.Ancho - 0.035)   
          end  
 --  
      when isnull(a.CantidadProducto,'0')='0' then   
       --    
       case when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.025)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.025)   
            when dop1.CodigoTubo='PALRZ00000026'    then (dop1.Ancho - 0.035)   
          end  
          --  
   when a.CantidadProducto=2 then   
       --  
          case when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.021)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.021)   
            when dop1.CodigoTubo='PALRZ00000026'    then (dop1.Ancho - 0.023)   
          end  
 --  
 end   
end   
  from  TBL_DetalleOrdenProduccion dop1   
LEFT OUTER join Tbl_DetalleOpGrupo lop1 on dop1.CotizacionGrupo = lop1.CotizacionGrupo  
left outer join Tbl_Ambiente a on dop1.IndiceAgrupacion=a.indice and a.NumeroCotizacion=lop1.NumeroCotizacion  
where dop1.Id=dop.Id -- limit 1  
)  
  
  
  
-- ROLLER BASICO TUBO  
  
WHEN   subString(dop.CodigoProducto,1,5)   ='PRTRB' THEN  
(  
select top 1  
case when dop1.Accionamiento='Manual' then  
  
   case when a.CantidadProducto=1 then   
   --  
          case  when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.025)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.025)    
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Ancho - 0.030)    
            when dop1.CodigoTubo='PALLU00000003'    then (dop1.Ancho - 0.030)    
          end  
 --  
      when isnull(a.CantidadProducto,'0')='0' then  
     --  
         case  when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.025)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.025)    
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Ancho - 0.030)    
            when dop1.CodigoTubo='PALLU00000003'    then (dop1.Ancho - 0.030)    
          end  
 --  
     when a.CantidadProducto=2 then  
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.021)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.021)    
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Ancho - 0.025)    
            when dop1.CodigoTubo='PALLU00000003'    then (dop1.Ancho - 0.025)      
          end  
 --  
     when a.CantidadProducto=3 then  
     --  
          case  when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.020)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.020)    
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Ancho - 0.021)    
          end  
 --   
    when a.CantidadProducto >= 4 then  
      
          case  when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.019)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.019)     
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Ancho - 0.021)    
          end  
   end      
    
end   
  from  TBL_DetalleOrdenProduccion dop1   
LEFT OUTER join Tbl_DetalleOpGrupo lop1 on dop1.CotizacionGrupo = lop1.CotizacionGrupo  
left outer join Tbl_Ambiente a on dop1.IndiceAgrupacion=a.indice and a.NumeroCotizacion=lop1.NumeroCotizacion  
where dop1.Id=dop.Id --limit 1  
)  
  
  
  
-- END  
-- Roller Shangrilla TUBO  
WHEN   subString(dop.CodigoProducto,1,5)   ='PRTSH' THEN  
(select  top 1   
case when dop1.Accionamiento='Manual' then  
  
   case when a.CantidadProducto=1 then   
   --  
          case  when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.030)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.030)   
          end  
 --  
        when isnull(a.CantidadProducto,'0')='0' then  
     --  
             case  when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.030)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.030)   
              end  
 --  
     when a.CantidadProducto=2 then  
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.021)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.021)   
          end  
 --  
     when a.CantidadProducto=3 then  
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.020)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.020)   
          end  
 --   
   end   
     
  when dop1.Accionamiento='Motorizado' then   
 case when a.CantidadProducto=1 then   
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.025)   
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.025)    
          end  
 --  
         when isnull(a.CantidadProducto,'0')='0' then   
       --  
           case when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.025)   
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.025)    
            end  
   when a.CantidadProducto=2 then   
       --  
          case when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.021)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.021)   
          end  
 --  
 end   
end     
  from  TBL_DetalleOrdenProduccion dop1   
LEFT OUTER join Tbl_DetalleOpGrupo lop1 on dop1.CotizacionGrupo = lop1.CotizacionGrupo  
left outer join Tbl_Ambiente a on dop1.IndiceAgrupacion=a.indice and a.NumeroCotizacion=lop1.NumeroCotizacion  
where dop1.Id=dop.Id --limit 1  
)  
ELSE '0'  
end as TuboCalculo,  
  
  
  
-- --TELA---- CONTINUAR DESDE AQUI MARISOL===============>  
case when   
 subString(dop.CodigoProducto,1,5)   ='PRTRS' then  
-- ROLLER SHADE   
(select  top 1  
case when dop1.Accionamiento='Manual' then  
  
   case when a.CantidadProducto=1 then   
   --  
          case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.025) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.025) -0.001)  
            when dop1.CodigoTubo='PALRS00000005'    then ((dop1.Ancho - 0.030) -0.001)  
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Ancho - 0.028) -0.001)  
            when dop1.CodigoTubo='PALLU00000003'    then ((dop1.Ancho - 0.030) -0.001)   
            -- when dop1.CodigoTubo='PALLU00000004'    then ((dop1.Ancho - 0.030) -0.001)   
          end  
 --  
        when isnull(a.CantidadProducto,'0')='0' then  
     --  
          case  when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.025) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.025) -0.001)  
            when dop1.CodigoTubo='PALRS00000005'    then ((dop1.Ancho - 0.030) -0.001)  
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Ancho - 0.028) -0.001)  
            when dop1.CodigoTubo='PALLU00000003'    then ((dop1.Ancho - 0.030) -0.001)   
            -- when dop1.CodigoTubo='PALLU00000004'    then ((dop1.Ancho - 0.030) -0.001)   
            end  
 --  
     when a.CantidadProducto=2 then  
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.023) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.023) -0.001)  
            when dop1.CodigoTubo='PALRS00000005'    then ((dop1.Ancho - 0.025) -0.001)  
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Ancho - 0.023) -0.001)  
            when dop1.CodigoTubo='PALLU00000003'    then ((dop1.Ancho - 0.025) -0.001)    
          end  
 --  
     when a.CantidadProducto=3 then  
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.021) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.021) -0.001)  
            when dop1.CodigoTubo='PALRS00000005'    then ((dop1.Ancho - 0.021) -0.001)  
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Ancho - 0.021) -0.001)   
          end  
 --  
     when a.CantidadProducto >=4 then   
      --  
           case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.021) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.021) -0.001)  
            when dop1.CodigoTubo='PALRS00000005'    then ((dop1.Ancho - 0.021) -0.001)  
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Ancho - 0.021) -0.001)   
          end   
   end   
     
  when dop1.Accionamiento='Motorizado' then   
 case when a.CantidadProducto=1 then   
     --  
          case when dop1.CodigoTubo='PALRS00000003'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.025) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.025) -0.001)  
            when dop1.CodigoTubo='PALRS00000005'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.030) -0.001)  
            when dop1.CodigoTubo='PALRZ00000026'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.028) -0.001)   
            when dop1.CodigoTubo='PALLU00000003'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.030) -0.001)   
              
              
            -- RAEX  
              
            when dop1.CodigoTubo='PALRS00000005'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then ((dop1.Ancho - 0.038) -0.001)               
            when dop1.CodigoTubo='PALRS00000024'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then ((dop1.Ancho - 0.038) -0.001)               
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then ((dop1.Ancho - 0.040) -0.001)   
            -- LUTRON  
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTLU00000002,MOTLU00000004,MOTLU00000003,MOTLU00000001')>0   then ((dop1.Ancho - 0.040) -0.001)   
              
          end  
 --  
      when isnull(a.CantidadProducto,'0')='0' then   
       --  
           case when dop1.CodigoTubo='PALRS00000003'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.025) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.025) -0.001)  
            when dop1.CodigoTubo='PALRS00000005'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.030) -0.001)  
            when dop1.CodigoTubo='PALRZ00000026'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.028) -0.001)   
            when dop1.CodigoTubo='PALLU00000003'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.030) -0.001)   
              
            when dop1.CodigoTubo='PALRS00000005'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then ((dop1.Ancho - 0.038) -0.001)               
            when dop1.CodigoTubo='PALRS00000024'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then ((dop1.Ancho - 0.038) -0.001)               
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then ((dop1.Ancho - 0.040) -0.001)    
            -- LUTRON  
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTLU00000002,MOTLU00000004,MOTLU00000003,MOTLU00000001')>0   then ((dop1.Ancho - 0.040) -0.001)   
              
          end  
 --  
   when a.CantidadProducto=2 then   
       --  
          case when dop1.CodigoTubo='PALRS00000003'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.023) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.023) -0.001)  
            when dop1.CodigoTubo='PALRS00000005'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.025) -0.001)  
            when dop1.CodigoTubo='PALRZ00000026'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.023) -0.001)   
            when dop1.CodigoTubo='PALLU00000003'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.025) -0.001)   
              
            -- RAEX  
            when dop1.CodigoTubo='PALRS00000005'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then ((dop1.Ancho - 0.029) -0.001)               
            when dop1.CodigoTubo='PALRS00000024'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then ((dop1.Ancho - 0.029) -0.001)               
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then ((dop1.Ancho - 0.040) -0.001)    
            -- LUTRON  
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTLU00000002,MOTLU00000004,MOTLU00000003,MOTLU00000001')>0   then ((dop1.Ancho - 0.040) -0.001)   
              
          end  
 --  
    when a.CantidadProducto=3 then   
       --  
          case when dop1.CodigoTubo='PALRS00000003'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.021) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.021) -0.001)  
            when dop1.CodigoTubo='PALRS00000005'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.021) -0.001)  
            when dop1.CodigoTubo='PALRZ00000026'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.021) -0.001)               
              
            -- RAEX   
            when dop1.CodigoTubo='PALRS00000005' and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0    then   
          -- --FORMULA  
    case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    ((dop1.Ancho - 0.030)-0.001)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    ((dop1.Ancho - 0.030)-0.001)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    ((dop1.Ancho - 0.020)-0.001)  
    end   
   when dop1.CodigoTubo='PALRS00000024'   and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then   
          -- --FORMULA  
     case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    ((dop1.Ancho - 0.030)-0.001)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    ((dop1.Ancho - 0.030)-0.001)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where       dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    ((dop1.Ancho - 0.020)-0.001)  
    end   
          -- END ((dop1.Ancho - 0.029) -0.001)   
          when  dop1.CodigoTubo='PALLU00000003' and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0    then   
           case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    ((dop1.Ancho - 0.030)-0.001)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    ((dop1.Ancho - 0.030)-0.001)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    ((dop1.Ancho - 0.020)-0.001)  
    end   
    when  dop1.CodigoTubo='PALLU00000003' and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTLU00000002,MOTLU00000004,MOTLU00000003,MOTLU00000001')>0    then   
           case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    ((dop1.Ancho - 0.040)-0.001)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    ((dop1.Ancho - 0.040)-0.001)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    ((dop1.Ancho - 0.040)-0.001)  
    end    
                  
          end  
 --  
      
     when a.CantidadProducto>=4 then   
       --  
          case when dop1.CodigoTubo='PALRS00000003'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.021) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.021) -0.001)  
            when dop1.CodigoTubo='PALRS00000005'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.021) -0.001)  
            when dop1.CodigoTubo='PALRZ00000026'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.021) -0.001)    
            when dop1.CodigoTubo='PALRS00000005'   and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then -- --FORMULA  
     case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    ((dop1.Ancho - 0.030)-0.001)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    ((dop1.Ancho - 0.030)-0.001)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    ((dop1.Ancho - 0.020)-0.001)  
    end   
   when dop1.CodigoTubo='PALRS00000024'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then -- --FORMULA  
     case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    ((dop1.Ancho - 0.030)-0.001)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    ((dop1.Ancho - 0.030)-0.001)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    ((dop1.Ancho - 0.020)-0.001)  
    end   
          -- END ((dop1.Ancho - 0.029) -0.001)   
            when  dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then   
           case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    ((dop1.Ancho - 0.030)-0.001)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    ((dop1.Ancho - 0.030)-0.001)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    ((dop1.Ancho - 0.020)-0.001)  
    end   
   when  dop1.CodigoTubo='PALLU00000003' and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTLU00000002,MOTLU00000004,MOTLU00000003,MOTLU00000001')>0    then   
           case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    ((dop1.Ancho - 0.040)-0.001)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    ((dop1.Ancho - 0.040)-0.001)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    ((dop1.Ancho - 0.040)-0.001)  
    end    
                  
          end  
            
 end  
 --   
end    
  from  TBL_DetalleOrdenProduccion dop1   
LEFT OUTER join Tbl_DetalleOpGrupo lop1 on dop1.CotizacionGrupo = lop1.CotizacionGrupo  
left outer join Tbl_Ambiente a on dop1.IndiceAgrupacion=a.indice and a.NumeroCotizacion=lop1.NumeroCotizacion  
where dop1.Id=dop.Id -- limit 1  
)  
  
  
  
-- ROLLER BASICO TELA  
WHEN   subString(dop.CodigoProducto,1,5)   ='PRTRB' THEN  
(  
select top 1  
case when dop1.Accionamiento='Manual' then  
  
   case when a.CantidadProducto=1 then   
   --  
          case  when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.025) - 0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.025) - 0.001)   
            when dop1.CodigoTubo='PALRS00000005'    then ((dop1.Ancho - 0.030) - 0.001)  
            when dop1.CodigoTubo='PALLU00000003'    then ((dop1.Ancho - 0.030) - 0.001)  
          end  
 --  
      when isnull(a.CantidadProducto,'0')='0' then  
     --  
          case  when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.025) - 0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.025) - 0.001)   
            when dop1.CodigoTubo='PALRS00000005'    then ((dop1.Ancho - 0.030) - 0.001)  
            when dop1.CodigoTubo='PALLU00000003'    then ((dop1.Ancho - 0.030) - 0.001)  
          end  
          --  
     when a.CantidadProducto=2 then  
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.021) - 0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.021) - 0.001)    
            when dop1.CodigoTubo='PALRS00000005'    then ((dop1.Ancho - 0.025) - 0.001)  
            when dop1.CodigoTubo='PALLU00000003'    then ((dop1.Ancho - 0.025) - 0.001)  
          end  
 --  
     when a.CantidadProducto=3 then  
     --  
          case  when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.020) - 0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.020) - 0.001)    
            when dop1.CodigoTubo='PALRS00000005'    then ((dop1.Ancho - 0.021) - 0.001)   
          end  
 --   
    when a.CantidadProducto >= 4 then  
      
          case  when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.019) - 0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.019) - 0.001)    
            when dop1.CodigoTubo='PALRS00000005'    then ((dop1.Ancho - 0.021) - 0.001)   
          end  
   end      
    
end   
  from  TBL_DetalleOrdenProduccion dop1   
LEFT OUTER join Tbl_DetalleOpGrupo lop1 on dop1.CotizacionGrupo = lop1.CotizacionGrupo  
left outer join Tbl_Ambiente a on dop1.IndiceAgrupacion=a.indice and a.NumeroCotizacion=lop1.NumeroCotizacion  
where dop1.Id=dop.Id -- limit 1  
)  
  
  
-- END  
--  Roller Zebra  TELA  
WHEN   subString(dop.CodigoProducto,1,5)   ='PRTRZ' THEN  
(select top 1  
case when dop1.Accionamiento='Manual' then  
 -- N  
   case when a.CantidadProducto=1 then   
   --  
          case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.030) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.030) -0.001)   
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Ancho - 0.030) -0.001)   
    end  
 --  
     when isnull(a.CantidadProducto,'0')='0' then  
     --  
           case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.030) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.030) -0.001)   
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Ancho - 0.030) -0.001)   
          end  
 --  
     when a.CantidadProducto=2 then  
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.021) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.021) -0.001)   
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Ancho - 0.023) -0.001)   
          end  
 --  
     when a.CantidadProducto=3 then  
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.020) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.020) -0.001)   
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Ancho - 0.021) -0.001)   
          end  
 --  
   end   
     
  when dop1.Accionamiento='Motorizado' then   
 case when a.CantidadProducto=1 then   
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.025) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.025) -0.001)   
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Ancho - 0.035) -0.001)   
          end  
 --  
      when isnull(a.CantidadProducto,'0')='0' then   
       --  
           case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.025) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.025) -0.001)   
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Ancho - 0.035) -0.001)   
          end  
          --   
   when a.CantidadProducto=2 then   
       --  
          case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.021) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.021) -0.001)   
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Ancho - 0.023) -0.001)   
          end  
 --  
 end   
end    
  from  TBL_DetalleOrdenProduccion dop1   
LEFT OUTER join Tbl_DetalleOpGrupo lop1 on dop1.CotizacionGrupo = lop1.CotizacionGrupo  
left outer join Tbl_Ambiente a on dop1.IndiceAgrupacion=a.indice and a.NumeroCotizacion=lop1.NumeroCotizacion  
where dop1.Id=dop.Id -- limit 1  
)  
  
  
-- Roller Shangrilla TELA  
WHEN   subString(dop.CodigoProducto,1,5)   ='PRTSH' THEN  
(select top 1  
case when dop1.Accionamiento='Manual' then  
  
   case when a.CantidadProducto=1 then   
   --  
          case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.030) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.030) -0.001)   
          end  
 --  
     when isnull(a.CantidadProducto,'0')='0' then  
     --  
         case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.030) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.030) -0.001)   
          end  
 --  
     when a.CantidadProducto=2 then  
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.021) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.021) -0.001)   
          end  
 --  
     when a.CantidadProducto=3 then  
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.020) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.020) -0.001)   
          end  
 --  
   end   
     
  when dop1.Accionamiento='Motorizado' then   
 case when a.CantidadProducto=1 then   
     --  
          case  when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.025) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.025) -0.001)   
          end  
 --   
       when isnull(a.CantidadProducto,'0')='0' then   
       --  
          case  when dop1.CodigoTubo='PALRS00000003'   then ((dop1.Ancho - 0.021) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.021) -0.001)   
          end  
 --  
   when a.CantidadProducto=2 then   
       --  
          case  when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.021) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.021) -0.001)   
          end  
 --  
 end   
end     
  from  TBL_DetalleOrdenProduccion dop1   
LEFT OUTER join Tbl_DetalleOpGrupo lop1 on dop1.CotizacionGrupo = lop1.CotizacionGrupo  
left outer join Tbl_Ambiente a on dop1.IndiceAgrupacion=a.indice and a.NumeroCotizacion=lop1.NumeroCotizacion  
where dop1.Id=dop.Id  
-- limit 1  
)  
  
ELSE '0'  
end as TelaCalculo,  
  
-- --ALTURA----  
case when   
 subString(dop.CodigoProducto,1,5)   ='PRTRS' then  
-- ROLLER SHADE   
(  
select  top 1  
case when dop1.Accionamiento='Manual' then  
  
   case when a.CantidadProducto=1 then   
   --  
          case   
   when dop1.CodigoTubo='PALRS00000003'    then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALRZ00000026'    then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALLU00000003'    then (dop1.Alto + 0.30)    
          end  
 --    
    when isnull(a.CantidadProducto,'0')='0' then  
     --  
         case   
   when dop1.CodigoTubo='PALRS00000003'    then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALRZ00000026'    then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALLU00000003'    then (dop1.Alto + 0.30)    
          end  
 --  
     when a.CantidadProducto=2 then  
     --  
         case   
   when dop1.CodigoTubo='PALRS00000003'    then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALRZ00000026'    then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALLU00000003'    then (dop1.Alto + 0.30)   
          end  
 --  
     when a.CantidadProducto=3 then  
     --  
          case   
   when dop1.CodigoTubo='PALRS00000003'    then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALRZ00000026'    then (dop1.Alto + 0.25)  
               
          end  
 --  
     when a.CantidadProducto >=4 then   
      --  
          case   
   when dop1.CodigoTubo='PALRS00000003'    then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALRZ00000026'    then (dop1.Alto + 0.25)  
              
          end   
            
 --  
   end   
     
  when dop1.Accionamiento='Motorizado' then   
 case when a.CantidadProducto=1 then   
     --  
          case   
            when dop1.CodigoTubo='PALRS00000003'   and dop1.CodigoMotor='MOTRZ00000002' then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000004'  and dop1.CodigoMotor='MOTRZ00000002'  then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000005'   and dop1.CodigoMotor='MOTRZ00000002' then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALRZ00000026'   and dop1.CodigoMotor='MOTRZ00000002' then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALLU00000003'  and dop1.CodigoMotor='MOTRZ00000002'  then (dop1.Alto + 0.30)  
            -- RAEX  
              
            when dop1.CodigoTubo='PALRS00000005'   and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALRS00000024'   and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then (dop1.Alto + 0.30)  
            -- LUTRON              
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTLU00000002,MOTLU00000004,MOTLU00000003,MOTLU00000001')>0  then (dop1.Alto + 0.30)  
          end  
  --  
       when isnull(a.CantidadProducto,'0')='0' then   
       --  
          case   
            when dop1.CodigoTubo='PALRS00000003'   and dop1.CodigoMotor='MOTRZ00000002' then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000004'  and dop1.CodigoMotor='MOTRZ00000002'  then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000005'   and dop1.CodigoMotor='MOTRZ00000002' then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALRZ00000026'   and dop1.CodigoMotor='MOTRZ00000002' then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALLU00000003'  and dop1.CodigoMotor='MOTRZ00000002'  then (dop1.Alto + 0.30)  
              
            when dop1.CodigoTubo='PALRS00000005'   and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALRS00000024'   and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then (dop1.Alto + 0.30)  
            -- LUTRON              
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTLU00000002,MOTLU00000004,MOTLU00000003,MOTLU00000001')>0  then (dop1.Alto + 0.30)  
  end  
 --  
   when a.CantidadProducto=2 then   
       --  
          case   
            when dop1.CodigoTubo='PALRS00000003'   and dop1.CodigoMotor='MOTRZ00000002' then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000004'  and dop1.CodigoMotor='MOTRZ00000002'  then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000005'   and dop1.CodigoMotor='MOTRZ00000002' then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALRZ00000026'   and dop1.CodigoMotor='MOTRZ00000002' then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALLU00000003'  and dop1.CodigoMotor='MOTRZ00000002'  then (dop1.Alto + 0.30)  
            -- RAEX              
            when dop1.CodigoTubo='PALRS00000005'   and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALRS00000024'   and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then (dop1.Alto + 0.30)  
            -- LUTRON              
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTLU00000002,MOTLU00000004,MOTLU00000003,MOTLU00000001')>0  then (dop1.Alto + 0.30)  
          end  
 --  
      when a.CantidadProducto=3 then   
       --  
          case   
            when dop1.CodigoTubo='PALRS00000003'   and dop1.CodigoMotor='MOTRZ00000002' then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000004'  and dop1.CodigoMotor='MOTRZ00000002'  then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000005'   and dop1.CodigoMotor='MOTRZ00000002' then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALRZ00000026'   and dop1.CodigoMotor='MOTRZ00000002' then (dop1.Alto + 0.25)  
              
            when dop1.CodigoTubo='PALRS00000005'   and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0 then   
             -- --FORMULA  
    case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    (dop1.Alto + 0.25)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    (dop1.Alto + 0.25)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    (dop1.Alto + 0.25)  
    end   
    when dop1.CodigoTubo='PALRS00000024' and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then   
             -- --FORMULA  
    case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    (dop1.Alto + 0.25)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    (dop1.Alto + 0.25)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    (dop1.Alto + 0.25)  
    end   
          -- END (dop1.Alto + 0.25)   
            when  dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then   
           case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    (dop1.Alto + 0.30)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    (dop1.Alto + 0.30)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    (dop1.Alto + 0.30)  
    end   
    when  dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTLU00000002,MOTLU00000004,MOTLU00000003,MOTLU00000001')>0   then   
           case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    (dop1.Alto + 0.30)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    (dop1.Alto + 0.30)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    (dop1.Alto + 0.30)  
    end   
          end  
 --  
      when a.CantidadProducto>=4 then   
       --  
          case   
            
            when dop1.CodigoTubo='PALRS00000003'   and dop1.CodigoMotor='MOTRZ00000002' then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000004'  and dop1.CodigoMotor='MOTRZ00000002'  then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000005'   and dop1.CodigoMotor='MOTRZ00000002' then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALRZ00000026'   and dop1.CodigoMotor='MOTRZ00000002' then (dop1.Alto + 0.25)   
              
            when dop1.CodigoTubo='PALRS00000005' and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then   
             -- --FORMULA  
    case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    (dop1.Alto + 0.25)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    (dop1.Alto + 0.25)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    (dop1.Alto + 0.25)  
    end   
     when dop1.CodigoTubo='PALRS00000024' and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then   
             -- --FORMULA  
    case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    (dop1.Alto + 0.25)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    (dop1.Alto + 0.25)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    (dop1.Alto + 0.25)  
    end   
          -- END (dop1.Alto + 0.25)   
           when  dop1.CodigoTubo='PALLU00000003' and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then   
           case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    (dop1.Alto + 0.30)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    (dop1.Alto + 0.30)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    (dop1.Alto + 0.30)  
    end   
   when  dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTLU00000002,MOTLU00000004,MOTLU00000003,MOTLU00000001')>0   then   
           case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    (dop1.Alto + 0.30)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    (dop1.Alto + 0.30)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    (dop1.Alto + 0.30)  
    end   
          end  
 --  
 --  
 end   
end     
  from  TBL_DetalleOrdenProduccion dop1   
LEFT OUTER join Tbl_DetalleOpGrupo lop1 on dop1.CotizacionGrupo = lop1.CotizacionGrupo  
left outer join Tbl_Ambiente a on dop1.IndiceAgrupacion=a.indice and a.NumeroCotizacion=lop1.NumeroCotizacion  
where dop1.Id=dop.Id --limit 1  
)  
  
  
  
-- ROLLER BASICO ALTURA  
  
WHEN   subString(dop.CodigoProducto,1,5)   ='PRTRB' THEN  
(  
select top 1  
case when dop1.Accionamiento='Manual' then  
  
   case when a.CantidadProducto=1 then   
   --  
          case  when dop1.CodigoTubo='PALRS00000003'    then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Alto + 0.20)    
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Alto + 0.25)    
            when dop1.CodigoTubo='PALLU00000003'    then (dop1.Alto + 0.30)    
          end  
 --  
         when isnull(a.CantidadProducto,'0')='0' then  
     --  
       case  when dop1.CodigoTubo='PALRS00000003'    then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Alto + 0.20)    
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Alto + 0.25)    
            when dop1.CodigoTubo='PALLU00000003'    then (dop1.Alto + 0.30)    
          end  
 --  
     when a.CantidadProducto=2 then  
     --  
          case  when dop1.CodigoTubo='PALRS00000003'    then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000004'   then (dop1.Alto + 0.20)      
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Alto + 0.25)    
            when dop1.CodigoTubo='PALLU00000003'    then (dop1.Alto + 0.30)    
          end  
 --  
     when a.CantidadProducto=3 then  
     --  
          case   when dop1.CodigoTubo='PALRS00000003'    then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Alto + 0.20)     
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Alto + 0.25)   
          end  
 --   
    when a.CantidadProducto >= 4 then  
      
          case   when dop1.CodigoTubo='PALRS00000003'    then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Alto + 0.20)     
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Alto + 0.25)   
          end  
   end      
    
end   
  from  TBL_DetalleOrdenProduccion dop1   
LEFT OUTER join Tbl_DetalleOpGrupo lop1 on dop1.CotizacionGrupo = lop1.CotizacionGrupo  
left outer join Tbl_Ambiente a on dop1.IndiceAgrupacion=a.indice and a.NumeroCotizacion=lop1.NumeroCotizacion  
where dop1.Id=dop.Id  
)  
  
  
-- END  
--  Roller Zebra ALTURA  
WHEN   subString(dop.CodigoProducto,1,5)   ='PRTRZ' THEN  
(select top 1  
case when dop1.Accionamiento='Manual' then  
 -- N1  
   case when a.CantidadProducto=1 then   
   --  
          case  when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Alto*2) + 0.40)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Alto*2) + 0.40)   
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Alto*2) + 0.45)    
          end  
 --  
     when isnull(a.CantidadProducto,'0')='0' then  
     --  
         case  when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Alto*2) + 0.40)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Alto*2) + 0.40)   
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Alto*2) + 0.45)    
          end  
 --  
     when a.CantidadProducto=2 then  
     --  
          case   when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Alto*2) + 0.40)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Alto*2) + 0.40)   
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Alto*2) + 0.45)    
          end  
 --  
     when a.CantidadProducto=3 then  
     --  
          case   when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Alto*2) + 0.40)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Alto*2) + 0.40)   
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Alto*2) + 0.45)    
          end  
 --   
   end   
     
  when dop1.Accionamiento='Motorizado' then   
 case when a.CantidadProducto=1 then   
     --  
          case   when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Alto*2) + 0.40)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Alto*2) + 0.40)   
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Alto*2) + 0.45)    
          end  
 --  
      when isnull(a.CantidadProducto,'0')='0' then   
       --  
     case   when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Alto*2) + 0.40)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Alto*2) + 0.40)   
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Alto*2) + 0.45)    
          end  
   when a.CantidadProducto=2 then   
       --  
          case   when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Alto*2) + 0.40)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Alto*2) + 0.40)   
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Alto*2) + 0.45)    
          end  
 --  
 end   
end    
  from  TBL_DetalleOrdenProduccion dop1   
LEFT OUTER join Tbl_DetalleOpGrupo lop1 on dop1.CotizacionGrupo = lop1.CotizacionGrupo  
left outer join Tbl_Ambiente a on dop1.IndiceAgrupacion=a.indice and a.NumeroCotizacion=lop1.NumeroCotizacion  
where dop1.Id=dop.Id  
)  
  
-- Roller Shangrilla ALTURA  
WHEN   subString(dop.CodigoProducto,1,5)   ='PRTSH' THEN  
(  
select  top 1  
case when dop1.Accionamiento='Manual' then    
   case when a.CantidadProducto=1 then   
   --  
          case    
   when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Alto*2) + 0.40)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Alto*2) + 0.40)  
          end  
 --  
     when isnull(a.CantidadProducto,'0')='0' then  
     --  
         case    
   when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Alto*2) + 0.40)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Alto*2) + 0.40)  
          end  
 --  
     when a.CantidadProducto=2 then  
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Alto*2) + 0.40)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Alto*2) + 0.40)  
          end  
 --  
     when a.CantidadProducto=3 then  
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Alto*2) + 0.40)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Alto*2) + 0.40)  
          end  
 --   
   end   
     
  when dop1.Accionamiento='Motorizado' then   
 case when a.CantidadProducto=1 then   
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Alto*2) + 0.40)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Alto*2) + 0.40)  
          end  
 --  
      when isnull(a.CantidadProducto,'0')='0' then   
       --  
             case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Alto*2) + 0.40)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Alto*2) + 0.40)  
          end  
   when a.CantidadProducto=2 then   
       --  
          case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Alto*2) + 0.40)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Alto*2) + 0.40)  
          end  
 --  
 end   
end    
  from  TBL_DetalleOrdenProduccion dop1   
LEFT OUTER join Tbl_DetalleOpGrupo lop1 on dop1.CotizacionGrupo = lop1.CotizacionGrupo  
left outer join Tbl_Ambiente a on dop1.IndiceAgrupacion=a.indice and a.NumeroCotizacion=lop1.NumeroCotizacion  
where dop1.Id=dop.Id)  
ELSE '0'  
end as AlturaCalculo,  
--  
Dop.Id,  
OP.Cliente,  
Tc.Nombre as TipoCliente,   
 Dop.CodigoSisgeco ,  
    Dop.CodigoProducto,  
    Dop.NombreProducto,  
    Dop.UnidadMedida,  
 Dop.Familia,  
    Dop.SubFamilia,  
    Dop.Cantidad,  
    Dop.Alto,  
    Dop.Ancho,    
    Dop.AlturaCadena,    
    Dop.Accionamiento,   
 Dop.NumeroMotores,  
    Dop.Motor As NombreMotor,  
    Dop.CodigoMotor,  
    Dop.MarcaMotor,  
    Isnull(Dop.NombreTubo,'') as 'NombreTubo',    
 Dop.Mando,  
    Dop.IndiceAgrupacion,      
    Dop.IdTbl_Ambiente as 'Ambiente',  
    Dop.Turno,  
    /**new atr**/  
 Grd.FechaProduccion,  
 Dop.FechaEntrega ,  
 -- nota,    
(SELECT STUFF(  
               (SELECT ' ' + sb.nota  
                FROM TBL_DetalleOrdenProduccion sb  
                WHERE sb.CotizacionGrupo = dop.CotizacionGrupo  
                FOR XML PATH(''), TYPE  
               ).value('.', 'NVARCHAR(MAX)'),   
               1, 1, ''))        
      as Nota,   
    (SELECT STRING_AGG(sb.nota, ' ')   
     FROM TBL_DetalleOrdenProduccion sb  
     WHERE sb.CotizacionGrupo  = dop.CotizacionGrupo  
     GROUP BY sb.CotizacionGrupo) AS nota ,  
 Dop.CodigoTela,  
    Dop.Tela,  
 Dop.Color,  
 case when Dop.MandoAdaptador=0 then 'No' else 'Si'end as MandoAdaptador ,  
 case when Dop.LlevaBaston   =0 then 'No' else 'Si'end as MandoAdaptador  ,  
    Dop.Cenefa,  
    Dop.SoporteCentral,  
 case when Dop.SoporteCentral   =0 then 'No' else 'Si'end as SoporteCentral  ,   
 Dop.TipoMecanismo  as 'Tipo_Mec',  
 Dop.ModeloMecanismo  as 'Modelo_Mec',  
 Dop.TipoRiel as 'Riel',  
 Dop.TipoInstalacion  as 'Tipo_Instal',  
    Dop.TipoCadena as 'Tipo_Cadena',  
 Dop.TipoCassete as 'Cassete',  
 Dop.TipoSoporteCentral  as 'Tipo_Sop_Central',  
    Dop.Caida  as'Tipo_Caida',  
 Dop.TipoSuperior  as 'Tipo_Superior',  
 Dop.Lamina,  
 Dop.Apertura,  
 Dop.TipoCadena as 'Cadena_Inf',  
 Dop.MandoCordon  as 'Mando_Cordon',  
 Dop.MandoBaston  'Mando_Baston',  
 Dop.Cruzeta  as 'Cruzeta',  
 Dop.LlevaBaston  as 'L_Baston',  
    Dop.Apertura as 'Apertura',  
 '' as 'Tubo_medida',--PENDIENTE AGREGAR Y VALIDAR  
   '' as 'Tela_medida',--PENDIENTE AGREGAR Y VALIDAR  
    '' as 'Altura_medida',--PENDIENTE AGREGAR Y VALIDAR  
    Dop.NumeroCotizacion,  
 Dop.CotizacionGrupo ,  
      
    -- PERSIANA HORIZONTAL  
    Dop.BastonVarrilla,  
    '' as cabezales, --PENDIENTE AGREGAR Y VALIDAR  
    Dop.AlturaCordon,  
    Dop.Cordon,  
    Dop.CordonTipo2,  
    Dop.ViaRecogida,  
    Dop.Baston  
 ,  
    Dop.NumeroVias,  
    Dop.Riel as 'riel_dat',  
 (select  count(*) from TBL_DetalleOrdenProduccion dtpp where dtpp.Id=dop.Id  
 and dtpp.NombreProducto LIKE  '%M1%') as tipoToldo,  
 Dop.Dispositivo,  
 isnull(OP.Direccion,'') as Direccion,  
 isnull(OP.Telefono,'') as Telefono,  
 isnull(OP.NombreVendedor,'') as Vendedor,  
 -- select * from Tbl_OrdenProduccion  
 OP.FechaRegistro as Fecha_Fabricacion,  
 --isnull(Dop.central,'NO')  -- --AGREGAR CAMPO  
 '' AS Central,   
 -- isnull(dop.central_index,'NO') --AGREGAR CAMPO  
 '' As Central_index,  
 -- isnull(index_detalle,'') --AGREGAR CAMPO  
 '' As Index_detalle,  
 ROUND((Dop.Alto * Dop.Ancho),3) as area,  
 isnull(Ds.Nombre,'') as Destino,   
 isnull(Tp.Nombre,'') as Tipo_OP,  
Dop.FechaEntrega,  
Convert(varchar,getdate(),103) as FechaImpresion,  
-- Dop.Escuadra,   
'' EscruadraDet  ,
 
 ISNULL(
    (
      SELECT 
        STUFF(
          (
            SELECT ' <b>|</b> ' + '<b>(</b> ' + CAST(ees.Codigo AS VARCHAR(100)) + '<b> - </b>' + CAST(ees.Cantidad AS VARCHAR(100)) + ' <b>)</b> '
            FROM Tbl_Escuadra ees 
            WHERE ees.IdTBLDetalleOrdenProduccion = dop.Id
            FOR XML PATH(''), TYPE
          ).value('.', 'NVARCHAR(MAX)'), 
          1, 10, ''
        )
    ),
    ''
  ) AS EscruadraDet
   
  
from  TBL_DetalleOrdenProduccion Dop  
Join  Tbl_DetalleOpGrupo Grd ON Grd.CotizacionGrupo= Dop.CotizacionGrupo And Grd.NumeroCotizacion=Dop.NumeroCotizacion  
JOIN Tbl_OrdenProduccion OP on OP.NumeroCotizacion=Dop.NumeroCotizacion  
JOIN Tbl_Destino Ds on Ds.Id=OP.IdDestino   
JOIN Tbl_TipoOperacion Tp on Tp.Id=OP.IdTipoPeracion   
JOIN Tbl_TipoCliente Tc on Tc.Id=OP.TipoCliente   
WHERE  
substring(Dop.CodigoProducto,1,3)='PRT'   
-- Dop.Id <=27
--select Ancho - 0.025 from TBL_DetalleOrdenProduccion   
  
END  
GO


 ALTER PROCEDURE SP_GetOrdenProduccionDetalleGrupo  
    @NumeroCotizacion NVARCHAR(50)  
AS  
BEGIN  
  
select  
GRD.CotizacionGrupo,
GRD.FechaProduccion,
GRD.Turno,
E.Nombre  as EstadoPedido,
GRD.Tipo as TipoGrupo,
TP.Nombre as TipoOperacion, tc.Nombre  AS TipoCliente,
GRD.FechaProduccion, 
TPP.Nombre as NombreProyecto,
(select count(*) from TBL_DetalleOrdenProduccion dop where  substring(dop.CodigoProducto,1,5)='PRTRS' and dop.CotizacionGrupo=GRD.CotizacionGrupo) as  RS,
(select count(*) from TBL_DetalleOrdenProduccion dop where  substring(dop.CodigoProducto,1,5)='PRTRZ' and dop.CotizacionGrupo=GRD.CotizacionGrupo) as ZB,
(select count(*) from TBL_DetalleOrdenProduccion dop where  substring(dop.CodigoProducto,1,5)='PRTPH' and dop.CotizacionGrupo=GRD.CotizacionGrupo) as PH,
(select count(*) from TBL_DetalleOrdenProduccion dop where  substring(dop.CodigoProducto,1,5)='PRTCV' and dop.CotizacionGrupo=GRD.CotizacionGrupo) as CV,
(select count(*) from TBL_DetalleOrdenProduccion dop where  substring(dop.CodigoProducto,1,5)='PRTSH' and dop.CotizacionGrupo=GRD.CotizacionGrupo) as SH,
(select count(*) from TBL_DetalleOrdenProduccion dop where  substring(dop.CodigoProducto,1,5)='PRTCS' and dop.CotizacionGrupo=GRD.CotizacionGrupo) as CE,
(select count(*) from TBL_DetalleOrdenProduccion dop where  substring(dop.CodigoProducto,1,5)='PRTPJ' and dop.CotizacionGrupo=GRD.CotizacionGrupo) as PJ,
(select count(*) from TBL_DetalleOrdenProduccion dop where  substring(dop.CodigoProducto,1,3)<>'PRT' and dop.CotizacionGrupo=GRD.CotizacionGrupo) as OTROS,
OP.*
from Tbl_DetalleOpGrupo GRD
JOIN Tbl_OrdenProduccion OP on OP.NumeroCotizacion=GRD.NumeroCotizacion
JOIN Tbl_Estado E on E.Id=GRD.IdEstado
join Tbl_TipoOperacion TP on tp.Id=OP.IdTipoPeracion join tbl_tipocliente tc on  tc.Id=TipoCliente
join Tbl_Proyecto TPP on TPP.id=OP.idProyecto
    --WHERE GRD.NumeroCotizacion = @NumeroCotizacion  
END
GO

ALTER	 PROC Sp_ListarMantOP   
  @NumCotizacion VARCHAR(10)
  AS
  BEGIN	
SELECT 
    Op.Id,
    Op.NumeroCotizacion  as NumeroCotizacion,
    Op.CodigoSisgeco as  CodigoSisgeco,
    Op.RucCliente AS RucCliente,
    Op.Cliente,
    Op.FechaRegistro AS FechaCreacion,
    Op.FechaSap as  FechaProduccion,
    Op.nombreVendedor as NombreVendedor,
    Op.Distrito as Distrito
FROM  Tbl_OrdenProduccion Op  
END
GO

 ALTER PROC Sp_ListarOperacionesConstruccion  
  @Fecha VARCHAR(10)
  AS
  BEGIN	
select 
GRD.Id,
  GRD.NumeroCotizacion,GRD.CodigoSisgeco ,
  p.nombre as NombreProyecto,
  GRD.CotizacionGrupo,
  E.Nombre   as EstadoOp,
  TP.Nombre as TipoOperacion,
  op.RucCliente as RucCliente,
  op.cliente as NombreCliente,
  GRD.FechaCreacion AS FechaCreacion,
  GRD.FechaProduccion as FechaProduccion

  
from Tbl_DetalleOpGrupo GRD
JOIN Tbl_OrdenProduccion OP on OP.NumeroCotizacion=GRD.NumeroCotizacion
JOIN Tbl_Estado E on E.Id=GRD.IdEstado
join Tbl_TipoOperacion TP on tp.Id=OP.IdTipoPeracion 
join tbl_tipocliente tc on  tc.Id=TipoCliente
JOIN  tbl_proyecto p ON p.Id= op.IdProyecto

END
GO


 
 ALTER FUNCTION dbo.FIND_IN_SET
(
    @value NVARCHAR(MAX),
    @list NVARCHAR(MAX)
)
RETURNS INT
AS
BEGIN
    -- Declare variables
    DECLARE @pos INT = 0;
    DECLARE @index INT = 1;
    DECLARE @item NVARCHAR(MAX);
    
    -- Iterate through the list
    WHILE @index > 0
    BEGIN
        -- Get the position of the next comma
        SET @index = CHARINDEX(',', @list, @pos + 1);
        
        -- Get the item from the list
        SET @item = LTRIM(RTRIM(SUBSTRING(@list, @pos + 1, CASE WHEN @index > 0 THEN @index - @pos - 1 ELSE LEN(@list) END)));
        
        -- Check if the item matches the value
        IF @item = @value
        BEGIN
            RETURN 1; -- Return 1 if the value is found
        END
        
        -- Update the position
        SET @pos = @index;
    END
    
    -- Return 0 if the value is not found
    RETURN 0;
END


GO


-- EN BD SISGECO

Create PROCEDURE Sp_listarComponentesSisgeco
AS
BEGIN
SELECT
codigo
,[des]
,codunidad
FROM dbo.Articulo ar
WHERE ar.des NOT LIKE '%(Liquidado)%';
END
GO


