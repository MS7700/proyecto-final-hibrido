import * as React from "react";
import { List, Datagrid, TextField, BooleanField, Edit, Create, TextInput, SimpleForm, BooleanInput, NumberField,ReferenceField,NumberInput,ReferenceInput,SelectInput   } from 'react-admin';

const Filters = [
    <TextInput label="Cedula" source="Cedula" />,
    <TextInput label="Nombre"  source="Nombre" />,
    <NumberInput label="Salario"  source="Salario" />,
    <ReferenceInput label="Departamento"  source="DepartamentoID" reference="Departamento"><SelectInput label="Departamento"  optionText="Descripcion" /></ReferenceInput>,
    <ReferenceInput  label="Puesto" source="PuestoID" reference="Puesto"><SelectInput label="Puesto"  optionText="Descripcion" /></ReferenceInput>,
    <ReferenceInput label="Nomina" source="TipoNominaID" reference="TipoNomina"><SelectInput label="Nomina"  optionText="Descripcion" /></ReferenceInput>,
    <BooleanInput source="Estado" />
];

export const EmpleadoList = props => (
    <List {...props} filters={Filters}> 
        <Datagrid rowClick="edit">
            <TextField source="id" />
            <TextField source="Cedula" />
            <TextField source="Nombre" />
            <NumberField source="Salario" />
            <ReferenceField label="Departamento" source="DepartamentoID" reference="Departamento"><TextField label="Departamento"  source="Descripcion" /></ReferenceField>
            <ReferenceField label="Puesto" source="PuestoID" reference="Puesto"><TextField label="Puesto" source="Descripcion" /></ReferenceField>
            <ReferenceField  label="Nomina" source="TipoNominaID" reference="TipoNomina"><TextField  label="Nomina"  source="Descripcion" /></ReferenceField>
            <BooleanField source="Estado" />
        </Datagrid>
    </List>
);


export const EmpleadoEdit = props => (
    <Edit {...props}>
        <SimpleForm>
            <TextInput source="Cedula" />
            <TextInput source="Nombre" />
            <NumberInput source="Salario" />
            <ReferenceInput label="Departamento"  source="DepartamentoID" reference="Departamento"><SelectInput label="Departamento"  optionText="Descripcion" /></ReferenceInput>
            <ReferenceInput  label="Puesto" source="PuestoID" reference="Puesto"><SelectInput label="Puesto"  optionText="Descripcion" /></ReferenceInput>
            <ReferenceInput label="Nomina" source="TipoNominaID" reference="TipoNomina"><SelectInput label="Nomina"  optionText="Descripcion" /></ReferenceInput>
            <BooleanInput source="Estado" />
        </SimpleForm>
    </Edit>
);

export const EmpleadoCreate = props => (
    <Create {...props}>
        <SimpleForm>
            <TextInput source="Cedula" />
            <TextInput source="Nombre" />
            <NumberInput source="Salario" />
            <ReferenceInput label="Departamento"  source="DepartamentoID" reference="Departamento"><SelectInput label="Departamento"  optionText="Descripcion" /></ReferenceInput>
            <ReferenceInput  label="Puesto" source="PuestoID" reference="Puesto"><SelectInput label="Puesto"  optionText="Descripcion" /></ReferenceInput>
            <ReferenceInput label="Nomina" source="TipoNominaID" reference="TipoNomina"><SelectInput label="Nomina"  optionText="Descripcion" /></ReferenceInput>
            <BooleanInput source="Estado" />
        </SimpleForm>
    </Create>
);
