# Guía de Despliegue y Arquitectura en Kubernetes (k8s)

Esta documentación detalla la arquitectura de orquestación, los manifiestos aplicados y la bitácora de resolución de incidentes para el despliegue del proyecto **Gestion Licitaciones XP** en Kubernetes local.

---

## 1. Arquitectura del Clúster

El proyecto está configurado dentro del namespace `licitaciones-namespace` e integra los siguientes componentes:

* **Base de Datos:** Deployment / StatefulSet de PostgreSQL (`postgres-0`) con volumen persistente (`PVC`).
* **Backend .NET API:** Deployment de la aplicación (`app-deployment`) configurado a **1 réplica** para evitar condiciones de carrera en las migraciones de Entity Framework Core.
* **Secrets:** `app-secret` para la gestión segura de cadenas de conexión (`ConnectionStrings__DefaultConnection`).
* **Services:** `app-service` (tipo `LoadBalancer` / `NodePort`) para exponer el backend hacia el clúster.

---

## 2. Manifiesto del Deployment (`k8s/app-deployment.yaml`)

```yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  name: app-deployment
  namespace: licitaciones-namespace
spec:
  replicas: 1
  selector:
    matchLabels:
      app: licitaciones-app
  template:
    metadata:
      labels:
        app: licitaciones-app
    spec:
      containers:
        - name: licitaciones-app
          image: licitaciones-app:latest
          imagePullPolicy: IfNotPresent
          ports:
            - containerPort: 8080
              name: http
          env:
            - name: ASPNETCORE_ENVIRONMENT
              value: "Development"
            - name: ConnectionStrings__DefaultConnection
              valueFrom:
                secretKeyRef:
                  name: app-secret
                  key: ConnectionStrings__DefaultConnection
          resources:
            requests:
              memory: "128Mi"
              cpu: "100m"
            limits:
              memory: "512Mi"
              cpu: "500m"



Comandos de Verificación y Uso
Aplicar cambios en el clúster
PowerShell
kubectl apply -f k8s/app-deployment.yaml
Consultar estado de los Pods
PowerShell
kubectl get pods -n licitaciones-namespace
Crear túnel local (Port-Forwarding)
PowerShell
kubectl port-forward pod/<NOMBRE_DEL_POD_ACTIVO> 8080:8080 -n licitaciones-namespace