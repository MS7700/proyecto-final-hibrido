import { ViewColumn } from "@material-ui/icons";
import React, { useState,useEffect } from "react";
import { List, Datagrid, TextField, BooleanField, Edit, Create, TextInput, SimpleForm, BooleanInput, NumberField,ReferenceField,NumberInput,ReferenceInput,SelectInput,DateField,DateInput, FormDataConsumer,RadioButtonGroupInput   } from 'react-admin';
import { useQuery, Loading, Error,useDataProvider,useGetOne } from 'react-admin';
import { useForm,useFormState } from 'react-final-form';


export const TransaccionList = props => (
    <List {...props}>
        <Datagrid rowClick="edit">
            <TextField source="id" />
            <NumberField source="EmpleadoID" />
            <DateField source="Fecha" />
            <TextField source="Tipo" />
            <NumberField source="TipoIngresoID" />
            <TextField source="TipoDeduccionID" />
            <NumberField source="Monto" />
            <BooleanField source="Contabilizado" />
        </Datagrid>
    </List>
);


export const TransaccionEdit = props => (
    <Edit {...props}>
        <SimpleForm>
            <NumberInput source="EmpleadoID" />
            <DateInput source="Fecha" />
            <TextInput source="Tipo" />
            
            <ReferenceInput source="TipoIngresoID" reference="TipoIngreso">
                <SelectInput optionText="Nombre" />
            </ReferenceInput>
            <ReferenceInput source="TipoDeduccionID" reference="TipoDeduccion">
                <SelectInput optionText="Nombre"  />
            </ReferenceInput>
            <NumberInput source="Monto" />
            <BooleanInput source="Contabilizado" />
        </SimpleForm>
    </Edit>
);

const tipos = [{id:"Ingreso",name:"Ingreso"},{id:"Deducción",name:"Deducción"}];

const GetData = (id,recurso) => {
    const { data, loading, error } = useGetOne(recurso, id);
    return data ? data : undefined;
}


const MontoInput = ({ EmpleadoID, tipoID, tipoResource }) => {
    
    const empleado = GetData(EmpleadoID,"Empleado");
    const tipo = GetData(tipoID,tipoResource);
    
   //fetch(`/Empleado()`);
    //const tipo = data;
    const form = useForm();
    let monto = 0;
    if(empleado && tipo){
        if(tipo.Porcentual){
            
            monto = empleado.Salario * (tipo.Cantidad / 100);
            
        }else{
            monto = tipo.Cantidad;
        }
        if(tipo.Automatico){
            form.change("Monto",monto);
        }
        return (
            <NumberInput label="Monto" source="Monto" />
        )
    }
    return(<div></div>)

};


export const TransaccionCreate = props => {
    
    //const forceUpdate = useForceUpdate();
    return (<Create {...props}>
        <SimpleForm>
            <NumberInput source="EmpleadoID"  />
            <DateInput source="Fecha" />
            <RadioButtonGroupInput label="Tipo" source="Tipo" choices={tipos} />
            <FormDataConsumer>
                {({formData, ...rest}) => formData.Tipo === 'Ingreso' &&
                    <ReferenceInput label="TipoIngreso" source="TipoIngresoID" reference="TipoIngreso" o>
                        <SelectInput optionText="Nombre" />
                    </ReferenceInput>
                }
            </FormDataConsumer>
            <FormDataConsumer>
                {({formData, ...rest}) => formData.Tipo === 'Deducción' &&
                    <ReferenceInput label="TipoDeduccion" source="TipoDeduccionID" reference="TipoDeduccion"  >
                        <SelectInput optionText="Nombre" />
                    </ReferenceInput>
                }
            </FormDataConsumer>
            <FormDataConsumer>
                {({formData, ...rest}) =>  formData.EmpleadoID && formData.TipoIngresoID &&
                    <MontoInput EmpleadoID={formData.EmpleadoID} tipoID={formData.TipoIngresoID} tipoResource="TipoIngreso" />
                }
            </FormDataConsumer>
            <FormDataConsumer>
                {({formData, ...rest}) =>  formData.EmpleadoID && formData.TipoDeduccionID &&
                    <MontoInput EmpleadoID={formData.EmpleadoID} tipoID={formData.TipoDeduccionID} tipoResource="TipoDeduccion" />
                }
            </FormDataConsumer>
            <BooleanInput source="Contabilizado" />
        </SimpleForm>
    </Create>)
}
/*
<RadioButtonGroupInput label="Tipo" source="Tipo" choices={tipos} onChange={ChangeTipo(values,form)}/>
            <FormDataConsumer>
                {({formData, ...rest}) => formData.Tipo === 'Ingreso' &&
                    <ReferenceInput label="TipoIngreso" source="TipoIngresoID" reference="TipoIngreso" o>
                        <SelectInput optionText="Nombre" />
                    </ReferenceInput>
                }
            </FormDataConsumer>
            <FormDataConsumer>
                {({formData, ...rest}) => formData.Tipo === 'Deducción' &&
                    <ReferenceInput label="TipoDeduccion" source="TipoDeduccionID" reference="TipoDeduccion"  >
                        <SelectInput optionText="Nombre" />
                    </ReferenceInput>
                }
            </FormDataConsumer>
            <FormDataConsumer>
                {({formData, ...rest}) =>  formData.EmpleadoID && formData.TipoIngresoID &&
                    <MontoInput EmpleadoID={formData.EmpleadoID} tipoID={formData.TipoIngresoID} tipoResource="TipoIngreso" />
                }
            </FormDataConsumer>
            */