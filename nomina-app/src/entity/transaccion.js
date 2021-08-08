
import { List, Datagrid, TextField, BooleanField, Edit, Create,  SimpleForm, 
    NumberField,ReferenceField,NumberInput,ReferenceInput,SelectInput,DateField,
    DateInput, FormDataConsumer,RadioButtonGroupInput,Toolbar,SaveButton   } from 'react-admin';
import { useGetOne } from 'react-admin';
import { useForm } from 'react-final-form';
import * as React from "react";
import Button from '@material-ui/core/Button';
import { Link } from 'react-router-dom';

const MySaveButton = ({props}) =>(
    <SaveButton {...props}
        redirect={false}
    />
);

const CustomToolbar = props => (
    <Toolbar {...props} >
        <MySaveButton />
    </Toolbar>
);


export const TransaccionList = props => (
    <List {...props}>
        <Datagrid rowClick="edit">
            <TextField source="id" />
            <ReferenceField label="Empleado" source="EmpleadoID" reference="Empleado" >
                <TextField source="Nombre" />
            </ReferenceField>
            <DateField source="Fecha" />
            <TextField source="Tipo" />
            <ReferenceField label="Tipo de Ingreso" source="TipoIngresoID" reference="TipoIngreso" >
                <TextField source="Nombre" />
            </ReferenceField>
            <ReferenceField label="Tipo de Deducción" source="TipoDeduccionID" reference="TipoDeduccion" >
                <TextField source="Nombre" />
            </ReferenceField>
            <NumberField source="Monto" />
            <BooleanField source="Contabilizado" />
        </Datagrid>
    </List>
);


export const TransaccionEdit = props => (
    <Edit  {...props} transform={transform}>
        <SimpleForm toolbar={<Toolbar alwaysEnableSaveButton />}>
            <ReferenceInput label="Empleado"  source="EmpleadoID" reference="Empleado"  >
                <SelectInput label="Empleado"  optionText="Nombre" />
            </ReferenceInput>
            <DateInput source="Fecha" />
            <RadioButtonGroupInput label="Tipo" source="Tipo" choices={tipos} />
            <FormDataConsumer subscription={{ values: true }}>
                {({formData, ...rest}) => formData.Tipo && <TipoInput tipo={formData.Tipo} />
                }
            </FormDataConsumer>
            <FormDataConsumer >
                {({formData, ...rest}) =>  formData.EmpleadoID && (formData.TipoIngresoID || formData.TipoDeduccionID) &&
                    <MontoInput EmpleadoID={formData.EmpleadoID} tipoID={formData.Tipo === 'Ingreso' ? formData.TipoIngresoID : formData.TipoDeduccionID} tipoResource={"Tipo"+formData.Tipo} />
                }
            </FormDataConsumer>
        </SimpleForm>
    </Edit>
);

const tipos = [{id:"Ingreso",name:"Ingreso"},{id:"Deducción",name:"Deducción"}];

const GetData = (id,recurso) => {
    const { data, loading, error } = useGetOne(recurso, id);
    return data ? data : undefined;
}


const TipoInput = ({tipo}) => {
    let myTipo;
    if(tipo === "Deducción"){
        myTipo = "Deduccion";
    }else{
        myTipo = tipo;
    }
    return(
        <ReferenceInput label={"Tipo de " + tipo} source={"Tipo"+myTipo +"ID"} reference={"Tipo"+myTipo } >
            <SelectInput label={"Tipo"+myTipo} optionText="Nombre" />
        </ReferenceInput>
    )
    
};

const MontoInput = ({ EmpleadoID, tipoID, tipoResource }) => {
    let mytipoResource;
    if(tipoResource === "TipoDeducción"){
        mytipoResource = "TipoDeduccion";
    }else{
        mytipoResource = tipoResource;
    }
    const empleado = GetData(EmpleadoID,"Empleado");
    const tipo = GetData(tipoID,mytipoResource);
    
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

const transform = data => {
    if(data.Tipo && data.Tipo==="Ingreso"){
        console.log("Ingreso");
        return ({
            ...data,
            TipoDeduccionID: null,
            Contabilizado: false,
        })
    }else if(data.Tipo && data.Tipo==="Deducción"){
        console.log("Deducción");
        return ({
            ...data,
            TipoIngresoID: null,
            Contabilizado: false,
        })
    }
    console.log(data);
    return data;
}
export const TransaccionCreate = props => {
    
    return (<Create {...props} transform={transform}>
        <SimpleForm  redirect={false}>
            <ReferenceInput label="Empleado"  source="EmpleadoID" reference="Empleado"  >
                <SelectInput label="Empleado"  optionText="Nombre" />
            </ReferenceInput>
            <DateInput source="Fecha" />
            <RadioButtonGroupInput label="Tipo" source="Tipo" choices={tipos} />
            <FormDataConsumer subscription={{ values: true }}>
                {({formData, ...rest}) => formData.Tipo && <TipoInput tipo={formData.Tipo} />
                }
            </FormDataConsumer>
            <FormDataConsumer >
                {({formData, ...rest}) =>  formData.EmpleadoID && (formData.TipoIngresoID || formData.TipoDeduccionID) &&
                    <MontoInput EmpleadoID={formData.EmpleadoID} tipoID={formData.Tipo === 'Ingreso' ? formData.TipoIngresoID : formData.TipoDeduccionID} tipoResource={"Tipo"+formData.Tipo} />
                }
            </FormDataConsumer>
        </SimpleForm>
    </Create>)
}
