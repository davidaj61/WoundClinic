from django.db import models
from Patient.models import Patient,PatientService
from django.contrib.auth.models import User



class Dressing(models.Model):
    name = models.CharField(max_length=100)
    has_price = models.BooleanField(default=False)
    price = models.DecimalField(max_digits=10, decimal_places=2, null=True, blank=True)

    def __str__(self):
        return self.name
    
class WoundService(models.Model):
    patient_service = models.ForeignKey(PatientService, on_delete=models.CASCADE)
    dressing = models.ForeignKey(Dressing, on_delete=models.CASCADE)
    quantity = models.PositiveIntegerField()
    price = models.DecimalField(max_digits=10, decimal_places=2)

    def total_price(self):
        return self.quantity * self.price

    def __str__(self):
        return f"{self.dressing.name} x {self.quantity}"

class Prescription(models.Model):
    user = models.ForeignKey(User, on_delete=models.SET_NULL, null=True)
    patient = models.ForeignKey(Patient, on_delete=models.CASCADE)
    dressing = models.ForeignKey(Dressing, on_delete=models.CASCADE)
    quantity = models.PositiveIntegerField()
    price = models.DecimalField(max_digits=10, decimal_places=2)

    def total_price(self):
        return self.quantity * self.price

    def __str__(self):
        return f"Prescription for {self.patient}"   