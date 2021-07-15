import * as React from "react";
import { List, Datagrid, TextField, BooleanField, Edit, Create, TextInput, SimpleForm, BooleanInput, NumberField,ReferenceField,NumberInput,ReferenceInput,SelectInput   } from 'react-admin';

const postFilters = [
    <TextInput label="Cedula" source="Cedula" />,
    <TextInput label="Nombre"  source="Nombre" />,
    <NumberInput label="Salario"  source="Salario" />,
    <ReferenceInput  source="DepartamentoId" reference="Departamentos"><SelectInput label="Departamento"  optionText="departamento" /></ReferenceInput>,
    <ReferenceInput source="PuestoId" reference="Puestos"><SelectInput label="Puesto"  optionText="puesto" /></ReferenceInput>,
    <ReferenceInput source="NominaId" reference="Nominas"><SelectInput label="Nomina"  optionText="nomina" /></ReferenceInput>
];

export const EmpleadoList = props => (
    <List {...props} filters={postFilters}> 
        <Datagrid rowClick="edit">
            <TextField source="id" />
            <TextField source="Cedula" />
            <TextField source="Nombre" />
            <NumberField source="Salario" />
            <ReferenceField source="DepartamentoId" reference="Departamentos"><TextField source="departamento" /></ReferenceField>
            <ReferenceField source="PuestoId" reference="Puestos"><TextField source="puesto" /></ReferenceField>
            <ReferenceField source="NominaId" reference="Nominas"><TextField source="nomina" /></ReferenceField>
        </Datagrid>
    </List>
);


export const EmpleadoEdit = props => (
    <Edit {...props}>
        <SimpleForm>
            <TextInput source="Cedula" />
            <TextInput source="Nombre" />
            <NumberInput source="Salario" />
            <ReferenceInput source="DepartamentoId" reference="Departamentos"><SelectInput optionText="departamento" /></ReferenceInput>
            <ReferenceInput source="PuestoId" reference="Puestos"><SelectInput optionText="puesto" /></ReferenceInput>
            <ReferenceInput source="NominaId" reference="Nominas"><SelectInput optionText="nomina" /></ReferenceInput>
        </SimpleForm>
    </Edit>
);

export const EmpleadoCreate = props => (
    <Create {...props}>
        <SimpleForm>
            <TextInput source="Cedula" />
            <TextInput source="Nombre" />
            <NumberInput source="Salario" />
            <ReferenceInput source="DepartamentoId" reference="Departamentos"><SelectInput optionText="departamento" /></ReferenceInput>
            <ReferenceInput source="PuestoId" reference="Puestos"><SelectInput optionText="puesto" /></ReferenceInput>
            <ReferenceInput source="NominaId" reference="Nominas"><SelectInput optionText="nomina" /></ReferenceInput>
        </SimpleForm>
    </Create>
);
